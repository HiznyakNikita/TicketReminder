   M S B A M L   `   `   `  ����  TicketReminder.    (TicketReminder.Showcase.UserNotification   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      \ XHardcodet.Wpf.TaskbarNotification, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  5      $	��143��5      $	��315��5        � 	Resources  5
       �� MergedDictionaries �� Source 
  FadeIn g� 	BeginTime �� 
TargetName �� TargetProperty O� KeyTime O� Value  HighlightCloseButton  FadeCloseButton  FadeBack  FadeOut�� 5   
    5      �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $e _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?�6%   �� 5      $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?�6%   �� 5      $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   %   &	        &	     &	     &	     &	 �    �� 5      .��5      U� $ 00:00:00X�60   $
 grid��5   ,   $ (UIElement.Opacity)�5   ,   .\�5      �� $ 00:00:00��6+   $ 0Q�5   '   �� 5      $ 00:00:01��6+   $
 0.95Q�5   '   �� 5      $ 00:00:03��6+   $
 0.95Q�5   '   5!      5"      �� 5#      .��5$      U� $ 00:00:00X�60   $ imgClose��5%   ,   $ (UIElement.Opacity)�5&   ,   .\�5'      �� $ 00:00:00��6+   $	 0.4Q�5(   '   �� 5)      $ 00:00:00.3000000��6+   $ 1Q�5*   '   5+      5,      �� 5-      .��5.      U� $ 00:00:00X�60   $ imgClose��5/   ,   $ (UIElement.Opacity)�50   ,   .\�51      �� $ 00:00:00��6+   $ 1Q�52   '   �� 53      $ 00:00:00.3000000��6+   $	 0.4Q�54   '   55      56      �� 57      .��58      U� $ 00:00:00X�60   $
 grid��59   ,   $ (UIElement.Opacity)�5:   ,   .\�5;      �� $ 00:00:00��6+   $ 1Q�5<   '   �� 5=      $ 00:00:00.3000000��6+   $ 1Q�5>   '   5?      5@      �� 5A      -   5        .��5C      U� $ 00:00:00X�60   $
 grid��5D   ,   $ (UIElement.Opacity)�5E   ,   .\�5F      �� $ 00:00:00��6+   $ 1Q�5G   '   �� 5H      $ 00:00:00.3000000��6+   $	 0.2Q�5I   '   5J      5K      5L       � Triggers 5N      :� 5O   
   	 :� RoutedEvent$#	 tb:TaskbarIcon.BalloonShowing��6   .[�5P      �� -   5        
 ��Name$
 FadeIn_BeginStoryboard��5Q      #��[  5P      5Q      5R      :� 5S   
   $	 Mouse.MouseEnter��6    :� 
SourceName$ imgClose��5T      .[�5U      �� -   5        $*
 $HighlightCloseButton_BeginStoryboard��5V      #��[ 5U      5V      5W      :� 5X   
   $	 Mouse.MouseLeave��6   $ imgClose��5Y      .[�5Z      �� -   5        $%
 FadeCloseButton_BeginStoryboard��5[      #��[ 5Z      5[      5\      :� 5]   
   $	 Mouse.MouseEnter��6   .[�5^      ��  �� BeginStoryboardName$ FadeIn_BeginStoryboard��6   �� 5_      -   5        $
 FadeBack_BeginStoryboard1��5_      #��[ 5`      5a      :� 5b   
   $#	 tb:TaskbarIcon.BalloonClosing��6   .[�5c      �� -   5        $
 FadeOut_BeginStoryboard��5d      #��[ 5c      5d      5e      .��5i      � -   5         �Name$
 grid��5i      $��0,0,-4,0q�5j   (   .+�5k   
   �� $��Stretch=�6   $��5,5,5,5q�5l      $��1,1,1,1q�5m      ���	#FF377599���	#FFFBFAFA J� Effect 5o      3  -System.Windows.Media.Effects.DropShadowEffect  5p         Color$ 	#FF747474��6#   5r      �� 5s   
   $
��Left=�6   $��	10,13,0,0q�5t      $��67��5u      $
��Fill=�5w      $��71��5x      $	��Top=�5y      $0��*/TicketReminder;component/Icons/ticket.ico��6(   �� 5z   
   -   5         ��Name$ MainText��5z      $��
82,52,10,0q�6&   $	��Top=�5{      ���	#FF006FCB$��10(�5}      $
��Wrap=�5~      �� 5      $>�8This is a user control. The animation uses the attached ��6    �� 5�      $��Italic$�6   $
��Bold"�5�      $�BalloonShowing ��5�      5�      F� 5�   
   ���	#FFFFFFFF �� Stretch$
 Fill=�5�      $��82,52,7.667,0q�5�      $	��Top=�5�      $��1��5�      ���Ѐ����`����`	 J� SnapsToDevicePixels . ��5�      �� 5�       �� EndPoint$ 	0.973,0.5&�6&    �� 
StartPoint$ 	0.005,0.5&�5�   &   .��5�      �  � Color$ 	#00ECAD25��6#    � Offset$ 1Q�5�   #   � 5�      $ 	#872592EC��6#   $ 0.673Q�5�   #   5�      5�      �� 5�   
   -	   5        $ HeaderBalloon��5�      $��
82,10,35,0q�5�       �� TextAlignment$ Center=�6(   $	��Top=�5�      $��40��5�      $��14(�5�      $��123123��5�      $
��Wrap=�5�      ���	#FF006FCB$
��Bold"�5�      �� 5�   
   -
   5         ��Name$ imgClose��5�      $��Right=�5�      $��	0,10,10,0q�5�      $	��Top=�5�      $��20��5�      $��22��5�       J� Opacity$	 0.7Q�5�      $
��Fill=�5�       � ToolTip$ Close Balloon��5�      $0��*/TicketReminder;component/Images/close.png��5�   /   F� 5�   
   ���	#FFFFFFFF$
 Fill=�5�   
   $��
10,97,10,0q�5�   
   $	��Top=�5�   
   $��1��5�   
   ���Ѐ����`����`	 . ��5�      �� 5�      $ 	0.973,0.5&�6&   $ 	0.005,0.5&�5�      .��5�      � $ 	#00ECAD25��6#   $ 1Q�5�      � 5�      $ 	#872592EC��6#   $ 0.931Q�5�      5�      5�      �� 5�   
   -   5         ��Name$ btnShowTicket��5�      $��Ticket��6(   $
��Left=�69   $��234,103,0,0q�6T   $	��Top=�6i   $��75��6�   6�   5�      5�      