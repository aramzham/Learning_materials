<h2><strong>WPF and XAML examples</strong></h2>
<hr width="100%"/>
<img src="https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcSap38v4KPG9XyLiHuTnJU5HNfQdu70Mkx8f_bJ76V4-WcAU9v8" align="left" width="300px" height="200px"/>
<p>Windows Presentation Foundation (or WPF) is a graphical subsystem by Microsoft for rendering user interfaces in Windows-based applications. WPF, previously known as "Avalon", was initially released as part of .NET Framework 3.0. Rather than relying on the older GDI subsystem, WPF uses DirectX. WPF attempts to provide a consistent programming model for building applications and separates the user interface from business logic. It resembles similar XML-oriented object models, such as those implemented in XUL and SVG.
WPF employs XAML, an XML-based language, to define and link various interface elements. WPF applications can be deployed as standalone desktop programs or hosted as an embedded object in a website. WPF aims to unify a number of common user interface elements, such as 2D/3D rendering, fixed and adaptive documents, typography, vector graphics, runtime animation, and pre-rendered media. These elements can then be linked and manipulated based on various events, user interactions, and data bindings.</p>
<img src="https://www.synapseindia.com/UserFiles/XAML-LOGO.jpg" align="right" width="400px" height="250px"/>
<p>Extensible Application Markup Language, or XAML (pronounced "zammel"), is an XML-based markup language developed by Microsoft. XAML is the language behind the visual presentation of an application that you develop in Microsoft Expression Blend, just as HTML is the language behind the visual presentation of a Web page. Creating an application in Expression Blend means writing XAML code, either by hand or visually by working in the Design view of Expression Blend.</p>
<h3><i>Just a simple example of a XAML code</i></h3>

```xaml
<Window x:Class="BindNotControl.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:BindNotControl"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <Window.Resources>
        <local:MyClass x:Key="DataItem" MyProperty="Sample text in it"></local:MyClass>
    </Window.Resources>
    <Grid>
        <TextBlock Foreground="Blue" FontSize="40" Text="{Binding Source={StaticResource DataItem}, Path=MyProperty}"></TextBlock>
    </Grid>
</Window>
```

<p>WPF uses XAML for constructing visually stunning user interfaces (UI) in markup instead of in a programming language such as C#. You can create elaborate UI documents entirely in XAML by defining elements such as controls, text, images, shapes, animation, and more. Because XAML is declarative (like HTML), it requires the addition of code if you need to add run-time logic to your application.</p>
<hr width="100%"/>
<h2>Keywords</h2>
<ol>
  <li>Visual Studio 2015</li>
  <li>.Net Framework 4.6</li>
  <li>C# 6.0</li>
</ol>
