   M S B A M L   `   `   `  ����J  FKlickbot, Version=1.0.6856.19903, Culture=neutral, PublicKeyToken=null#clr-namespace:KlickbotKlickbot  5   	       Klickbot.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Klickbot     5   	   /tb http://www.hardcodet.net/taskbar     5   	     � Title$  Klickbot��5	   	   $��201.564��6   $��363.46��6+   $	��200��6:   $	��202��6J   $	��360��6Z   .��5
      �  � ColumnDefinitions 5   
   �� 5      .+�5   
   �� -   5         ��Name$ lblMousePosition��5      $�� ��6*   $��
10,0,10,74q�65   $��26��6I   $��Bottom=�6U   �� 5   
   -   5         ��Name$ 	txtHotkey��5       �� 
IsReadOnly . $��
10,0,10,49q�6a    �� TextWrapping$
 Wrap=�6u   $��20��6�   $��Bottom=�6�   �� 5   
   -   5        $ 	lblActive��5      $��
��6#   $��10,0,0,0q�68   $��42��6J   $��Bottom=�6V   $
��Left=�6q   $	��335��6�   �� 5   
   -   5        $ label��5      $��Mausklicks pro Sekunde:��6   $
��Left=�6A   $��
10,0,0,133q�6\   $��Bottom=�6p   $	��335��6�   �� 5   
   -   5        $ txtSpeed��5   f   $��10,0,10,110q�6x   $
 Wrap=�6�   $��18��6�   $��Bottom=�6�   ��6   ��  �� ElementName$ speed�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=�6�   5      5      