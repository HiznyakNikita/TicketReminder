   M S B A M L   `   `   `  ����  TicketReminder-    'TicketReminder.Showcase.ToolTipNoTicket   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      \ XHardcodet.Wpf.TaskbarNotification, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      $	��285��5      $	��136��5        � 	Resources  5	       
  FadeIn g� 	BeginTime �� 
TargetName �� TargetProperty O� KeyTime O� Value �� 	KeySpline  FadeOut��%[  &	        &	 �    �� 5
   
   .��5      U� $ 00:00:00X�6,   $ border��6A   $ (UIElement.Opacity)�6`   .\�5      �� $ 00:00:00��6'   $ 0Q�6:   �� 5      $ 00:00:01��6'   $ 1Q�6:   $ 0,0,0.46,0.969��6D   5      U� 5      $ 00:00:00X�6,   $ image��6A   $ (UIElement.Opacity)�6_   .\�5      �� $ 00:00:00��6'   $	 0.6Q�6:   �� 5      $ 00:00:01��6'   $ 1Q�6:   5      5      �� 5   
   .��5      U� $ 00:00:00X�6,   $ border��6A   $ (UIElement.Opacity)�6`   .\�5      �� $ 00:00:00��6'   $ 1Q�6:   �� 5      $ 00:00:01��6'   $
 0.25Q�6:   5      U� 5      $ 00:00:00X�6,   $ image��6A   $ (UIElement.Opacity)�6_   .\�5      �� $ 00:00:00��6'   $ 1Q�6:   �� 5      $ 00:00:01��6'   $	 0.6Q�6:   5      5      5       � Triggers 5      :� 5    
    :� RoutedEvent$" tb:TaskbarIcon.ToolTipOpened��6   .[�5!      �� -   5        	 ��Name$	 FadeIn_BeginStoryboard��5!   C   #��[  6   6C   5"      :� 5#   
   $! tb:TaskbarIcon.ToolTipClose��6   .[�5$      �� -   5        $	 FadeOut_BeginStoryboard��5$   D   #��[ 6   6D   5%      .��5(      � -   5        
 �Name$
 
LayoutRoot��5)      $
��Auto��6   .+�5*   
   �� -   5         ��Name$ border��51      $��Stretch=�5+      $��Stretch=�5,      $
��Auto��5-      $
��Auto��5.       �� CornerRadius$ 6,6,6,6��5/      $��3,3,3,3q�50      $��0,0,5,5q�51       J� Effect 52      3  -System.Windows.Media.Effects.DropShadowEffect  53         Color$ 	#FF7A7A7A��54      ��56      �� 57       �� EndPoint$ 0.5,1&�58       �� 
StartPoint$ 0.5,0&�59      .��5:      �  � Color$ 	#FF83E3FF��5;       � Offset$ 0Q�5<      � 5=      $ 	#FFFFFFFF��5>      $ 1Q�5?      5@      5B      �� 5C   
   -   5         ��Name$ image��5J   +   $
��Left=�5D      $��
10,10,0,26q�5E      $��72��5F      $0��*/TicketReminder;component/Icons/ticket.ico��5G      $
��Fill=�5H      $	��Top=�5I       J� RenderTransformOrigin$ 0.792,0.486&�5J      $��72��6:   z�5K      V� 5L      .~�5M      ��  �� ScaleX$ 1Q�6%    �� ScaleY$ 1Q�60   �� 5N       �� AngleX$ 0Q�6$    �� AngleY$ 0Q�6/   �� 5O       �� Angle$ 0Q�6&   T� 5P       T� X$ 0Q�6)    T� Y$ 0Q�6/   5Q      5S      �� 5T   
   -   5         ��Name$ tbHeader��5U      $��
82,10,20,0q�5V      $
��Wrap=�5W      $��32��5X      $	��Top=�5Y      $��16(�5Z      $
��Bold"�5[      ���	#FF575757�� 5\      $�No ticket for you...��5]   	    � Language$ de-ch�5^   	   6   �� 5_   
   -   5        $ txtToolTipDetail��5g      $��12(�5`      $
��Bold"�5a      ���	#FF141414$
��Wrap=�5c      $��
10,0,20,19q�5d      $��Bottom=�5e      ��5f      ��  �� ElementName$ me�� �� Path$ InfoText�5g      �� 5h   
   -   5        $ txtToolTipDetail_Copy��5n      $��12(�5i      $��Normal"�5j      ���	#FF141414$
��Wrap=�5l      $��82,42,20,44.96q�5m      �� 5n   '   $a�[Data binding is simple: The NotifyIcon sets the DataContext unless it's not already in use.��6+   $ de-ch�6�   6�   5o      5p      