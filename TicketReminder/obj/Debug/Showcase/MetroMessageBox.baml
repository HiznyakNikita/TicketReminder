   M S B A M L   `   `   `  ����  TicketReminder-    'TicketReminder.Showcase.MetroMessageBox   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $��MetroMessageWindow��5   	     � Title$  MetroMessageBox��5   	   $��147.733��5   	    � SizeToContent$ WidthAndHeight=�5   	    � WindowStartupLocation$ CenterScreen=�6(    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�65    � Topmost .  � AllowsTransparency . 
������  � 	Resources 5      	 �� MergedDictionaries
 �� Source   greenBackground �� EndPoint �� 
StartPoint � Color � Offset  yellowBackground  redBackground  blueBackground�� 5   
   	 5      �� 5      $F
 @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $I
 Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $e
 _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?�6%   �� 5      $M
 Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?�6%   �� 5      $R
 Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   �� 5      $
 Common.xaml?�6%   %h  &	        &	 �     &	 �    &	 p    �� 5      $ 0.5,1&�6:   $ 0.5,0&�6K   .��5      � $ 	#FF00BABA��6   $ 0.012Q�61   � 5      $ White��6   $ 1Q�6-   � 5      $ 	#FFF3F0F0��6   $ 0.389Q�61   5      �� 5      $ 0.5,1&�6;   $ 0.5,0&�6L   .��5      � $ 	#FFEFF56E��6   $ 0.009Q�61   � 5      $ White��6   $ 1Q�6-   � 5      $ 	#FFF0F3F0��6   $ 0.389Q�61   5       �� 5"      $ 0.5,1&�68   $ 0.5,0&�6I   .��5#      � $ 	#FFFF8A85��6   $ 0.012Q�61   � 5$      $ White��6   $ 1Q�6-   � 5%      $ 	#FFF3F0F0��6   $ 0.389Q�61   5&      �� 5(      $ 0.5,1&�69   $ 0.5,0&�6J   .��5)      � $ 	#FF6FA3FD��6   $ 0.012Q�61   � 5*      $ White��6   $ 1Q�6-   � 5+      $ 	#FFF3F0F0��6   $ 0.389Q�61   5,      5.      .��51      � -   5        $��mainGrid��51      ��52   
   �� 53      $ 0.5,1&�6"   $ 0.5,0&�63   .��54      � $ 	#FFD8FFFA��6   $ 0.012Q�61   � 55      $ White��6   $ 1Q�6-   � 56      $ 	#FFF3F0F0��6   $ 0.389Q�61   57      .+�5;   
   � $��36��6   $��	0,102,0,0q�6   $	��Top=�6.   .+�5<      �� -   5         ��Name$ btnOk��5<      $��Ok��6$   $
��Left=�61   $��	187,6,0,0q�6L   $	��Top=�6_   $��75��6w   6�   �� 5=      -   5        $ 	btnCancel��5=      $��Cancel��6(   $��	276,6,0,0q�69   $	��Top=�6L   $
��Left=�6z   $��74��6�   5>      �� 5@   
   $
��Left=�6   $��87��60   $��
104,10,0,0q�6<   $	��Top=�6P   $	��265��6h   .+�5A      �� -   5         ��Name$ MessageTitle��5A      $��123123��6.   
�������$��14(�6N    �� TextAlignment$ Center=�5B      $��0,0,0,5q�6/   $
��Bold"�6@   $��CharacterEllipsis=�6R   6u   �� 5C      -   5        $ textMessage��5C      $��123123��6-   $
��Wrap=�6;   $��
0,0,-0.8,0q�6O   5D      �� 5F   
   -   5         ��Name$ image��5F      $
��Left=�6   $��80��6:   $��	10,19,0,0q�6F   $	��Top=�6Y   $��80��6q   $/��)/TicketReminder;component/Icons/Train.ico��6|   5H      5I      