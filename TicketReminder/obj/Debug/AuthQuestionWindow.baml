   M S B A M L   `   `   `  ����I  ETicketReminder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TicketReminder.ShowcaseTicketReminder.Showcase  5   	   '    !TicketReminder.AuthQuestionWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   T PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls  5   	   1local%clr-namespace:TicketReminder.Showcase  5   	   \ XHardcodet.Wpf.TaskbarNotification, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  5   	     � AllowsTransparency  . 
������  � Title$ Welcome!��5   	   $��334.578��6   $��505.838��6+    � 
ResizeMode$ NoResize=�6;    � WindowStyle$
 None=�6Q    � WindowStartupLocation$ CenterScreen=�6d    � 	Resources 5	       
  FadeIn g� Duration �� TargetProperty W� From		 W� To  FadeOut  WindowBackgroundStyle
 �� 
TargetType �� Property �� Value  VisibleAnimation �� Triggers P� Property P� Value N� EnterActions��%4  &	        &	 i     &	 �     &	 �    �� 5   
   .��5      W� $ 0:0:1.35D�6   $ Opacity�62   $ 0X�6V   $	 1X�6_   5      �� 5   
   .��5      W� $ 0:0:.55D�6   $ Opacity�61   $	 0X�6U   5      �� 5   
   "
 ��5      .�5      ��  �@��CornerRadius$ 10��6-   �� 5       � ��$ 3q�60   �� 5       � ��$ 35q�6'   �� 5       � �� �	#FFF1F8FD6+   �� 5       � �� �	#FF6DA1CD6,   5      �� 5   
   "
 �6   .�5      ��  � y�$ 	Collapsed=�6+   �� 5       �@J�Opacity$ 0Q�6(    5       P� 5!       � y�$ Visible=�60    5"      �� 5#      .��5$      �� .��5%   "   W� $ Opacity�62   $	 0.0X�5&      $		 1.0X�6)   $ 0:0:0.2D�62   5'      5(      5*      5,      5.      .��50      �� #��[ 6   $
��Left=�6<   $��	35,35,0,0q�6W   $	��404��51   	   $	��227��6   $	��Top=�6"   ._�52   
   � $��-0.2,-0.2,-0.4,0.4q�6   .+�54      �� -   5         ��Name$ imageFly��54      $
��Left=�6&   $��62��6A   $��253,-18,-55,0q�55      $	��Top=�6+   $	��201��6C   $��Images/air_logo.png��56      �� 58      -   5        $ 
imageTrain��58      $
��Left=�6(   $��62��6C   $��-33,171,0,-12.4q�59      $	��Top=�6-   $	��123��6E   $��Images/train_logo.png��5:      3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  5>      -   5          Name$ MyNotification��5>         
IconSource$ /icons/Train.ico��5?         TrayToolTip 5@      -   'TicketReminder.Showcase.ToolTipNoTicket  5A       J� Opacity$
 0.85Q�6,      InfoText 6;   ��  �� Path$ ToolTipText� � ContextMenu 5D      �� 5E      -   5         ��Name$ contextMenuNotification��5E   "   .@�5F      m� -   5         m�Name$ menuMainWindow��5F   #   $��Main Window��6;    m� Icon 5H      �� 5I   "   $��16��6(   $��16��5J   $   5L      �� 5N      m� 5P      $��Windows��6#   .@�5Q      m� -   5        $ menuSearchTicket��5Q   '   $��Search ticket��6A    5S   "   �� 5T   &   $��16��6,   $��16��5U   $   5W      m� 5Y      -   5        $ menuShowSettingsApp��5Y   '   $��Settings application��6D    5[   "   �� 5\   &   $��16��6,   $��16��5]   $   5_      5`      �� 5b      m� 5d      -   5        $ menuNewTicketItme��5d   #   $��New ticket!��6>   {�.   5f      �� 5g   "   $��16��6(   $��16��5h   $   5j      �� 5l      m� 5n      -	   5        $ menuExit��5n   #   $
��Exit��65    5o      �� 5p   "   $��16��6(   $��16��5q   $   5s      5t      5v      �� 5y      $��Sign in dprc.gov.ua��6   $
��Wrap=�63   $
��Left=�5z      $��18(�63   $��	23,10,0,0q�6A   $	��Top=�5{      $	��239��60   $��52��6<   �� 5}      -
   5         ��Name$ 	btnSignIn��5}      $y�	Collapsed=�6(   $��Sign in��6?   $
��Left=�5~      $��
90,171,0,0q�60   $	��Top=�6D   $��98��6\   ���	#FF5AC1DF���	#FF6EC7E15   D   �� 5�      -   5        $ btnClose��5�      $	 100Q�6'   $y�	Collapsed=�65   $��Close��6L   $
��Left=�5�      $��207,171,0,0q�60   $	��Top=�6E   $��98��6]   $��26��5�   ,   ���	#FFECCCCC���	#FFCCCCCC6O   �� 5�      -   5        $ btnNextStep��5�      $ 0Q�6*   $��	Next step��66   $
��Left=�6J   $��253,171,0,0q�5�      $	��Top=�6*   $��98��6B   $��26��6M   ���	#FFECEDA86p   �� 5�      -   5         ��Name$ canvasSignIn��5�   ,   $��40,81,65.2,86q�6   $y�	Collapsed=�6B   .+�5�      �� -   5         ��Name$ tbLogin��5�      $�� ��6+   $
��Left=�5�      $��23��65    �� TextWrapping$
 Wrap=�6A   $	��Top=�5�      $	��186��62     �� Left$  80��6>   G� 5�      -   5        ! G�Name$! tbPass��5�      " G� Password$"  ��6.   $
��Left=�5�      $��23��69   $	��Top=�6E   $	��186��5�      $  80��6*   
# �� Top$# 31��6;   �� 5�      $
��Left=�6   $
��Wrap=�67   $��14(�5�      $��Login��6*   $	��Top=�67   $# 7��6O   �� 5�      $
��Left=�6   $
��Wrap=�67   $��Password��5�      $��14(�6,   $	��Top=�6:   $# 38��6R   5�      �� 5�      -   5        $ btnRegistration��5�      $ 0Q�6.   $��Regist��6:   $
��Left=�5�      $��126,171,0,0q�60   $	��Top=�6E   ���	#FFA8A8ED$��98��5�      $��26��6    ���	#FFA8A8ED6C   5�      5�      5�      