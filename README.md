# Xamarin.Forms Drag and Drop ListView
Hello!

At the time of writing this code, Xamarin.Forms does not support rows reordering and this is solution of problem.

**REMRAK:** This library implemented only for iOS, but i hope that Android version will be released soon.


# Demo (see example in repository)
## iOS
![](https://i.gyazo.com/1d6d0b7983fb403a95b34bbd60eb2884.gif)

# Usage
## Add dependencies
### PCL project
**MovableListView.dll**
### iOS project
**MovableListView.dll**

**MovableListView.iOS.dll**

## ExportRenderer
Add **ExportRenderer** line of code in your executable iOS poject.

**For example:**
```C#
[assembly: ExportRenderer(typeof(MovableViewCell), typeof(MovableViewCellRenderer))]
namespace YourProjectName.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate ....
```
## How to use in code (two easy steps)

### Use MovableListView.IObservableCollectionEx
*ListView.ItemsSource* collection must implement *MovableListView.IObservableCollectionEx*. You can use *MovableListView.ObservableCollectionEx* which provides all necessary methods and inherited from *ObservableCollection*.

### Use MovableViewCell
 Use MovableViewCell instead standart ViewCell.

*For example:*
```xaml
    <ListView x:Name ="lstView">
      <ListView.ItemTemplate>
        <DataTemplate>
          <movableListView:MovableViewCell>
            <ContentView BackgroundColor="White">
              <Label Text="{Binding Name}"/>
            </ContentView>
          </movableListView:MovableViewCell>
        </DataTemplate>
      </ListView.ItemTemplate>
    </ListView>
```
