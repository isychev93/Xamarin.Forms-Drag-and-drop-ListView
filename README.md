# Xamarin.Forms-Drag-and-drop-ListView
Hello!

At the time of writing this code, Xamarin.Forms does not support rows reordering and this is solution of problem.

REMRAK: This library implemented only for iOS, but i hope that Android version will be released soon.

# Demo
![](https://i.gyazo.com/1d6d0b7983fb403a95b34bbd60eb2884.gif)

# Usage
## Add dependencies
MovableListView to your Portable project.

MovableListView.iOS to your iOS project.

## ExportRenderer
```C#
[assembly: ExportRenderer(typeof(MovableViewCell), typeof(MovableViewCellRenderer))]
```

## Code example (see also in repository)
Instead standart ViewCell (as DataTemplate) use MovableViewCell.

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
ListView.ItemsSource collection must implement IObservableCollectionEx (see ObservableCollectionEx).
