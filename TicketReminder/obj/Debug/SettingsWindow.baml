   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	    TicketReminder#   TicketReminder.SettingsWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   -   5        (   "MahApps.Metro.Controls.MetroWindow   Name$  Settings��5       � Title$ Настройки��5   	   $��312.736��6   $��346.959��6,    � 
ResizeMode$ NoResize=�6<    � WindowStartupLocation$ CenterScreen=�6R    � 	Resources 5       �� MergedDictionaries �� Source�� 5	   
    5
      �� 5      $ /Resources/Icons.xaml?�6%   %    5      .��5      � .+�5   
   �� -   5         ��Name$ 
placeEmail��5      $��87,36,22,207q�6(   $��Type your email...��6>   
�������y�5      ��  �� ElementName$ tbEmail��	 �� Path$	 Text.IsEmpty�    BooleanToVisibilityConverter
 �� 	Converter#
 [  �� 5   
   -   5        $ 
placeLogin��5      $��87,168,22,76q�6(   $��Type your login...��6>   
�������y�5   
   �� *Text.IsEmpty+#
 [  $ tbLogin���� 5   
   $��Почта:��6   $
��Left=�6!   $��	10,31,0,0q�6<   $	��Top=�6O   �� 5   
   -   5         ��Name$ tbEmail��5      
������ $
��Left=�6<   $��23��6W   $��	81,34,0,0q�6c    �� TextWrapping$
 Wrap=�6v   $�� ��6�   $	��Top=�6�   $	��225��6�   �� 5    
   -   5         ��Name$ btnAddEmail��5       $��Добавить��6&   $
��Left=�69   $��
231,96,0,0q�6T   $	��Top=�6h   $��75��6�   6�   �� 5"   
   $��Логин:��6   $
��Left=�6!   $��
10,166,0,0q�6<   $	��Top=�6P   �� 5#   
   -   5        $ tbLogin��5#      
������ $
��Left=�6<   $��23��6W   $��
81,166,0,0q�6c   $
 Wrap=�6w   $�� ��6�   $	��Top=�6�   $	��225��6�   �� 5&   
   -   5        $ btnAddVkontakteAccount��5&      $��Добавить��61   $
��Left=�6D   $��231,228,0,0q�6_   $	��Top=�6t   $��75��6�   6�   �� 5'   
   $��Пароль:��6   $
��Left=�6"   $��
10,198,0,0q�6=   $	��Top=�6Q   G� 5)   
   -   5         G�Name$ passwordBoxVkPass��5)       G� Password$ 	123456789��61   $
��Left=�6F   $��
81,197,0,0q�6a   $	��Top=�6u   $	��225��6�   6�   �� 5*   
   $��Пароль:��6   $
��Left=�6"   $��	10,67,0,0q�6=   $	��Top=�6P   G� 5+   
   -	   5        $ passwordBoxEmailPass��5+      $ 	123456789��64   $
��Left=�6I   $��	81,66,0,0q�6d   $	��Top=�6w   $	��225��6�   6�   �� 5-   
   $
��Left=�6   $��
14,128,0,0q�6/   $
��Wrap=�6C   $��14(�6W   $��Add vkontakte account��5.      $	��Top=�61   �� 5/   
   $
��Left=�6   $��14,7,0,0q�6/   $
��Wrap=�6A   $��14(�6U   $��Add email account��50   
   $	��Top=�6#   �� 52   
   $
��Left=�6   
�������$��
14,258,0,0q�6A   $
��Wrap=�6U   $��14(�6i   $%��For more information go to help��53   
   $	��Top=�61   $	��276��6I   55         RightWindowCommands 57      +   %MahApps.Metro.Controls.WindowCommands  58   
   .@�5:      �� -
   5        $ btnHelpWindow��5:      .��5;      ��  �� Orientation$ 
Horizontal=�6   .+�5<      �� $��20��6    $��20��6+   ��5=      ��  �� RelativeSource ��  �� AncestorType$ ButtonL�$	 
Foreground� J� OpacityMask 5>      !� 5?       j� Stretch$
 Fill=�6*     appbar_page_search !� Visual# [ 5@      5B      �� 5C      $��4 0 0 0q�6    $��Center=�5D      $
��Help��5E      5F      5G      5H      5K      