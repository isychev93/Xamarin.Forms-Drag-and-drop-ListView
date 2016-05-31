using System;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace MovableListView.iOS
{
    public class MovableCellGestureRecognizer : UILongPressGestureRecognizer
    {
        private readonly WeakReference<ListView> weakListView;
        private readonly WeakReference<UITableView> weakTableView;
        private readonly WeakReference<UITableViewCell> weakNativeCell;
        private readonly WeakReference<MovableViewCell> weakCell;
        private NSIndexPath sourceOrNewAppliedIndexPath;
        private UIView opaqueView;

        private MovableCellGestureRecognizer(ListView listView, UITableView tableView, MovableViewCell cell, UITableViewCell nativeCell) : base(OnRecognizingAction)
        {
            weakListView = new WeakReference<ListView>(listView);
            weakTableView = new WeakReference<UITableView>(tableView);
            weakNativeCell = new WeakReference<UITableViewCell>(nativeCell);
            weakCell = new WeakReference<MovableViewCell>(cell);
        }

        public static MovableCellGestureRecognizer CreateGesture(ListView listView, UITableView tableView, MovableViewCell cell, UITableViewCell nativeCell)
        {
            return new MovableCellGestureRecognizer(listView, tableView, cell, nativeCell);
        }

        private static void OnRecognizingAction(UILongPressGestureRecognizer r)
        {
            var recognizer = r as MovableCellGestureRecognizer;
            if (recognizer == null)
                throw new InvalidOperationException(string.Format("UILongPressGestureRecognizer must be MovableCellGestureRecognizer ({0})", r.GetType()));

            recognizer.HandleRecognizerAction();
        }

        private void HandleRecognizerAction()
        {
            UITableView tableView;
            weakTableView.TryGetTarget(out tableView);
            UITableViewCell nativeCell;
            weakNativeCell.TryGetTarget(out nativeCell);
            ListView listView;
            weakListView.TryGetTarget(out listView);
            MovableViewCell cell;
            weakCell.TryGetTarget(out cell);
            if (tableView == null || nativeCell == null || listView == null || cell == null)
                return;
            var newPoint = LocationInView(tableView);
            var newRowIndexPath = tableView.IndexPathForRowAtPoint(newPoint);
            switch (State)
            {
                case UIGestureRecognizerState.Possible:
                    break;
                case UIGestureRecognizerState.Began:
                    if (cell.BeginReorderCommand != null)
                    {
                        tableView.BeginUpdates();
                        cell.BeginReorderCommand.Execute(new ReorderCommandParam(newRowIndexPath.Row, newRowIndexPath.Section, -1, -1));
                        tableView.EndUpdates();
                    }
                    if (newRowIndexPath != null)
                    {
                        sourceOrNewAppliedIndexPath = newRowIndexPath;
                        opaqueView = new UIView(new CGRect(new CGPoint(0, 0), nativeCell.Frame.Size)) { BackgroundColor = UIColor.Black.ColorWithAlpha(0.2f) };
                        nativeCell.AddSubview(opaqueView);
                    }
                    break;
                case UIGestureRecognizerState.Changed:
                    if (sourceOrNewAppliedIndexPath == null || newRowIndexPath == null)
                        break;

                    if (!Equals(sourceOrNewAppliedIndexPath, newRowIndexPath))
                    {
                        if (cell.CustomReorderCommaond == null)
                        {
                            if (listView.IsGroupingEnabled)
                            {
                                var groups = (IObservableCollectionEx)listView.ItemsSource;
                                var childrenOfNewGroup = (IObservableCollectionEx)groups[newRowIndexPath.Section];
                                if (sourceOrNewAppliedIndexPath.Section == newRowIndexPath.Section)
                                {
                                    childrenOfNewGroup.Move(sourceOrNewAppliedIndexPath.Row, newRowIndexPath.Row);
                                }
                                else
                                {
                                    var childrenOfSourceGroup =
                                        (IObservableCollectionEx)groups[sourceOrNewAppliedIndexPath.Section];
                                    var childrenToChangeOrder = childrenOfSourceGroup[sourceOrNewAppliedIndexPath.Row];
                                    tableView.BeginUpdates();
                                    childrenOfSourceGroup.Remove(childrenToChangeOrder);
                                    childrenOfNewGroup.Add(newRowIndexPath.Row, childrenToChangeOrder);
                                    tableView.EndUpdates();
                                }
                            }
                            else
                            {
                                var list = (IObservableCollectionEx)listView.ItemsSource;
                                list.Move(sourceOrNewAppliedIndexPath.Row, newRowIndexPath.Row);
                            }
                        }
                        else
                        {
                            tableView.BeginUpdates();
                            cell.CustomReorderCommaond.Execute(new ReorderCommandParam(sourceOrNewAppliedIndexPath.Row, sourceOrNewAppliedIndexPath.Section, newRowIndexPath.Row, newRowIndexPath.Section));
                            tableView.EndUpdates();
                        }

                        sourceOrNewAppliedIndexPath = newRowIndexPath;
                    }
                    break;
                case UIGestureRecognizerState.Ended:
                case UIGestureRecognizerState.Cancelled:
                case UIGestureRecognizerState.Failed:
                    if (cell.EndReorderCommand != null)
                    {
                        tableView.BeginUpdates();
                        cell.EndReorderCommand.Execute(new ReorderCommandParam(-1, -1, newRowIndexPath.Row, newRowIndexPath.Section));
                        tableView.EndUpdates();
                    }
                    if (opaqueView == null)
                        return;

                    opaqueView.RemoveFromSuperview();
                    opaqueView = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
