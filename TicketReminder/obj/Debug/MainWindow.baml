   M S B A M L   `   `   `  ����I  ETicketReminder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TicketReminder.ShowcaseTicketReminder.Showcase  5   	       TicketReminder.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   T PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls  5   	   4Showcase%clr-namespace:TicketReminder.Showcase  5   	   -   5        (  "MahApps.Metro.Controls.MetroWindow   Name$  TestMainWindow��5   K    � Title$ Search��6	   $��253.8��6   $��316.2��6'    � 
ResizeMode$ NoResize=�65    � 	Resources 5       �� MergedDictionaries �� Source�� 5	   
    5
      �� 5      $ /Resources/Icons.xaml?�6%   %    5         RightWindowCommands 5      +  %MahApps.Metro.Controls.WindowCommands  5   
   .@�5      �� -   5         ��Name$ btnHelpWindow��5      .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��20��6    $��20��6+   ��5      �� *
Foreground+	 �� RelativeSource	 �� 
 �� AncestorType"
 �� J� OpacityMask 5      !� 5       j� Stretch$
 Fill=�6*      appbar_page_search !� Visual# [  5      5      �� 5      $��4 0 0 0q�6    $��Center=�5      $
��Help��5      5      5       5!      .��5$      � .+�53   
   �� -   5        $ 	btnSearch��53      $��
Поиск��6$   $
��Left=�64   $��215,188,0,0q�6O   $	��Top=�6d   $��75��6|   6�   �� 55   
   -   5         ��Name$ cmbBoxPointFrom��55      
������  �� 
IsEditable . $
��Left=�6W   $��
142,40,0,0q�6r   $	��Top=�6�   $	��148��6�   �� 57   
   -   5        $ cmbBoxPointTo��57      
������  . $
��Left=�6U   $��
142,79,0,0q�6p   $	��Top=�6�   $	��148��6�   (  "System.Windows.Controls.DatePicker  58   
   -   5          Name$ datePickerDate��58      $
��Left=�6-   $��142,117,0,0q�6H   $	��Top=�6]   $	��148��6u   o� 59   
   $��31��6   $	��Top=�6   $��	0,0,0.4,0q�63   .@�5:      m� -   5         m�Name$ menuItemSettings��5:      $��Настройки��61   6D   m� 5;      -   5        $ menuItemCancelReserve��5;      $'��!Отмена резервации��66   6Q   5<      �� 5=   
   -	   5        $ cmbBoxTrainNumber��5=      
������ $
��Left=�6G   $��142,154,0,0q�6b   $	��Top=�6w   $	��148��6�    J� RenderTransformOrigin$ -1.314,3.32&�6�   �� 5>   
   -
   5         ��Name$ lblPointFrom��5>      $(��"Пункт отправления:��6&   $
��Left=�6C   $��	10,36,0,0q�6^   $	��Top=�6q   $	��123��6�   �� 5?   
   -   5        $ lblTo��5?      $"��Пункт прибытия:��6   $
��Left=�69   $��	10,75,0,0q�6T   $	��Top=�6g   $	��123��6   �� 5@   
   -   5        $ lblTrainNumber��5@      $��Номер поезда:��6(   $
��Left=�6@   $��
10,154,0,0q�6[   $	��Top=�6o   �� 5A   
   $&�� Дата отправления:��6   $
��Left=�6,   $��
10,115,0,0q�6G   $	��Top=�6[   5C      5E      