   M S B A M L   `   `   `  ����G  CSzperacz.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Szperacz.Wpf.IconsSzperacz.Wpf.Icons  5      =#clr-namespace:Szperacz.Wpf.ControlsSzperacz.Wpf.Controls  5      '    !Szperacz.Wpf.Controls.ExplorerBox   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /ic clr-namespace:Szperacz.Wpf.Icons     5      5local#clr-namespace:Szperacz.Wpf.Controls     5      -   5        $��ExplorerWindow��5
      .��5      � -   5          �Name$  gridExplorer��5       � ColumnDefinitions 5   
   �� 5      ��6   ��  �� ElementName$ combo�� �� Path$ ActualHeight��� 5      $��* �6   .+�5   
   �� -   5         ��Name$ combo��5      $��0��6"   $
��40 0q�62   ��5      �� *ExItemSource+$ ExplorerWindow�� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=���5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      #   Szperacz.Wpf.Icons.PhraseIcon  $��0 0 5 0q�6(   �� 5      ��6$   �� ��65   �� $ gridExplorer��$ ActualWidth�5      5      ��5      �� 5       �� 
TargetType" ��6   .�5      �� 	 �� Property	 � ��
 �� Value$
 0q�6/   �� 5      	 � ��$
 5 0 40 0q�60   5       5"      �� 5%   
   $��1��6   ._�5&      � .+�5'      �� 
�������6   �� 5*      -   5         ��Name$ textBlockWatermark��5*      
������ $��Center=�5+       �� Padding$	 5 6q�67   
���������5,      �� *TextWatermark+$ ExplorerWindow��$ TwoWay=�$ PropertyChanged=��� 5.      -   5         ��Name$ textBoxPath��5.       �� AutoWordSelection$
 True��6/   
������  �� TextWrapping$
 Wrap=�5/      $��Center=�6.   $��Center=�6I   $	��3 0q�51   R   ��52      �� *BorderColor+$ ExplorerWindow��$ TwoWay=�$ PropertyChanged=���53      �� $ gridExplorer��$ ActualHeight���54      �� *
TextInside+$ ExplorerWindow��$ TwoWay=�$ PropertyChanged=�.��6�   55      56      57      59      5:      