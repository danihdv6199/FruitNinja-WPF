   M S B A M L   `   `   `  ????B  >trabajo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:trabajotrabajo  5   	       trabajo.options   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:trabajo     5   	   -   5          ? Title$  options??5   	   $	??441??6   $	??657??6&    ? 
ResizeMode$ CanMinimize=?6I   .??5	      ? ???	#FFCFC9C9 ? RowDefinitions 5
   
   ?? 5      $
??368* ?6   ?? 5      $	??51* ?6   .+?5   
   &   System.Windows.Controls.DataGrid  -   5        $??DataGrid??5         AutoGenerateColumns .     SelectionMode$ Single=?6?      CanUserDeleteRows .     CanUserAddRows .  $	??362??6?   $	??Top=?6?   $	??651??6?   ???	#FFC9A8A8   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Tipo Figura??6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 3* 6:   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6E   ??  ?? Mode$ OneWay=? ?? Path$
 Name? ?? UpdateSourceTrigger$ PropertyChanged=?  5      $ Velocidad x??6%   $	 3* 6:   
 6E   ?? $ TwoWay=?$
 velX?$ PropertyChanged=?  5      $ Velocidad Y??6%   $	 3* 6:   
 6E   ?? $ TwoWay=?$
 velY?$ PropertyChanged=?5      ?? 5   
   -   5         ??Name$ EliminarFigura??5      $??1??6)   $??Eliminar??66   $??30??6h   $??281,10,276,10q?6t   $??Center=?6?   $??Center=?6?   $??94??6?   ???	#FF8594B66?   5      5      