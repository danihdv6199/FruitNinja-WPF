   M S B A M L   `   `   `  ????B  >trabajo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:trabajotrabajo  5   	       trabajo.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:trabajo     5   	   -   5          ? Title$  
MainWindow??5   	   $	??574??6   $	??800??6)    ? 
ResizeMode$ CanMinimize=?6T    ? Triggers 5	      :? 5
   
    :? RoutedEvent$ Window.Loaded??6   [?5      ?? 5      .??5      ??  ?? TargetProperty$ FontSize?6!    ?? 
TargetName$
 logo??6;   .??5      W?  W? From$	 100X?6*   	 W? To$	 110X?65    g? AutoReverse . 	 g? RepeatBehavior$	 Forever??6Q   
 g? Duration$
 0:0:1D?6j   5      5      5      ??5      ?? 5   
    ?? ImageSource$  Material/BackPrincipal.png??6   .??5      ? -   5         ?Name$ MainGrid??5       J? RenderTransformOrigin$ 0.762,0.573&?6   $??0,0,2,-2q?6A   .+?5   
   ?? -   5         ??Name$ BotonJugarAuto??5      $	??300??6)   $??50??65   $??	JuegoAuto??6A   $??25(?6U   $??250,247,250,153q?6c   6|   ?? 5   
   -   5        $??SegundosCombo??5       ?? Text$ Segundos??6(   $??Right=?68   $	??123??6T   $??0,306,106,207q?6`    ?? SelectedIndex$ 0??6w   .@?5      ?? -   5         ??Name$
 cbi1??5      $??30 Segundos??6)   ?? 5      -   5        $
 cbi2??5      $??45 Segundos??6)   ?? 5      -   5        $
 cbi3??5      $??60 Segundos??6)   5      ?? 5!   
   -   5        $??BotonJugarManual??5!      $	??300??6)   $??50??65   $??JugarManual??6A   $??25(?6W   $??250,319,250,81q?6e   6}   ?? 5"   
   -	   5        $??BotonOpciones??5"      $	??300??6&   $??50??62   $??Opciones??6>   $??25(?6Q   $??250,390,250,10q?6_   6w   ?? 5#   
   -
   5         ??Name$
 logo??5#   }   $??180,72,126,293q?6   $??
FruitNinja??6,   $	??100(?6>   
???????$??Center=?6`   $	??486??6?   5$      5&      