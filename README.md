# Material Chip View ![version](http://img.shields.io/badge/original-v1.0.1-brightgreen.svg?style=flat) [![NuGet Badge](https://buildstats.info/nuget/MaterialChipView)](https://www.nuget.org/packages/MaterialChipView/)

Port of [MaterialChipView](https://github.com/robertlevonyan/materialChipView) for Xamarin.Android

> Material Chip View can be used as tags for categories, contacts or creating text clouds.

## Setup

#### NuGet:

```
Install-Package MaterialChipView
```

## Usage

```xml
<MaterialChipView.Chip
    android:layout_width="wrap_content"
    android:layout_height="wrap_content"
    app:mcv_chipText="Chip Sample" />
```
![alt text](https://raw.githubusercontent.com/robertlevonyan/materialChipView/master/Images/sample.png)

### Cutomizing Chip

```xml
app:mcv_closable="true"
```
![alt text](https://raw.githubusercontent.com/robertlevonyan/materialChipView/master/Images/closable.png)

```xml
app:mcv_selectable="true"
```
![alt text](https://raw.githubusercontent.com/robertlevonyan/materialChipView/master/Images/selectable.png)

```xml
app:mcv_hasIcon="true"
app:mcv_chipIcon="@drawable/customIcon"
```
![alt text](https://raw.githubusercontent.com/robertlevonyan/materialChipView/master/Images/hasIcon.png)

```xml
app:mcv_hasIcon="true"
app:mcv_chipIcon="@drawable/customIcon"
app:mcv_closable="true"
```
![alt text](https://raw.githubusercontent.com/robertlevonyan/materialChipView/master/Images/hasIconClosable.png)

```xml
app:mcv_backgroundColor="@color/customChipBackgroundColor"
app:mcv_closeColor="@color/customCloseIconColor"
app:mcv_selectedBackgroundColor="@color/customSelectedChipColor"
app:mcv_textColor="@color/customTitleColor"
```

|![alt text](https://raw.githubusercontent.com/robertlevonyan/materialChipView/master/Images/custom1.png)|![alt text](https://raw.githubusercontent.com/robertlevonyan/materialChipView/master/Images/custom2.png)|
|----------------------------------------------------------------------------------------------|-----------|

### Attributes

|Custom Atributes             |Description                                 |
|-----------------------------|--------------------------------------------|
|`app:mcv_chipText`               |Text label of Chip                          |
|`app:mcv_textColor`              |Custom color for text label                 |
|`app:mcv_backgroundColor`        |Custom background color                     |
|`app:mcv_selectedBackgroundColor`|Custom background color when selected       |
|`app:mcv_hasIcon`                |Chip with icon                              |
|`app:mcv_chipIcon`               |Icon resource for Chip                      |
|`app:mcv_closable`               |Chip with close button                      |
|`app:mcv_closeColor`             |Custom color for close button               |
|`app:mcv_selectable`             |Chip with selection button                  |
|`app:mcv_selectedTextColor`      |Custom color for label when selected        |
|`app:mcv_selectedCloseColor`     |Custom color for close button when selected |

### Setting Handlers

```csharp
Chip chip = FindViewById<Chip>(Resource.Id.chip);
```
Chip click handler
```csharp
chip.Click += (sender, args) => {
	// Your action here...
};
```

On Close button click handler
```csharp
chip.Close += (sender, args) => {
	// Your action here...
};
```

On Icon click handler
```csharp
chip.IconClick += (sender, args) => {
	// Your action here...
};
```

On Select button click handler
```csharp
chip.Select += (sender, args) => {
	// Your action here...
};
```

### Customizing Chip from C#

```csharp
chip.ChipText // Chip label
chip.TextColor // Chip label color
chip.BackgroundColor // Custom background color
chip.SelectedBackgroundColor // Custom background color when selected
chip.HasIcon // Chip has icon
chip.ChipIcon // Icon Drawable for Chip
chip.Closable // Chip has close button
chip.CloseColor // Custom color for close button
chip.Selectable //Chip has selection button
chip.Clicked // Chip as clicked
chip.SelectedTextColor // Custom color for label when selected
chip.SelectedCloseColor // Custom color for close button when selected
```

## Versions

#### 1.0.1

Recreation issues are fixed

### 1.0.0

First version of library

## Licence

```
Material Chip View ©
Copyright 2017 Robert Levonyan, Yauheni Pakala
Original project: https://github.com/robertlevonyan/materialChipView
Port to Xamarin.Android: https://github.com/wcoder/MaterialChipView

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
```
