   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:DeskChatDeskChat  5   	       DeskChat.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:DeskChat     5   	   -   5          � Title$  
MainWindow��5   	   $��387.645��6   $��578.464��6-    � WindowStyle$
 None=�6=    � 
ResizeMode$ NoResize=�6P   .��5	      � -   5         �Name$ myGrid��5	      .+�5
   
   �� -   5         ��Name$ listView��5
      $
��Left=�6%   $��15��6@   $��609,328,-48,0q�6L   $	��Top=�6c   $��17��6{   ��6�   Z�  �� View 5      �� 5      .P�5      �� $	��Ola��6%   64   �� 5      .��5      4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Novo Header��61      CellTemplate 5      �� 5   "   .R�5   &   �� ��6,   �� 5   #   5      5      �� 5      -   5        col3��$
��Mail��5   3    �� Width$	 150��6A   	 �� CellTemplate	 5      �� 5      .R�5   "   �� ��6,   �� 5      5      5      5        � 5!   
   -   5        
  �Name$
 frame��5!      $�� ��6   $
��Left=�6*   $	��327��6E   $��0,61,0,0q�6R   $	��Top=�6d   $	��578��6|   �� 5"   
   -   5         ��Name$ rect2��5"      $
��Left=�6#   $��40��6>   $	��Top=�6J   $	��578��6b   $��0,21,0,0q�6n   ���	#FF1A237E6�   �� 5#   
   -   5        $ rect1��5#      $
��Left=�6#   $��21��6>   $	��Top=�6J   $	��578��6b   ���	#FF0000516n   �� 5$   
   -   5        $��548,1,10,368q�5$   T   ��6   Z� ��6'   Z� ��6>   Z� .��5%      F�    	closeIcon#��[  6    �� Stretch$ Uniform=�65   
�������6G   5&      �� 5'   
   -	   5        $��520,1,35,367q�5'      ��6'   Z� ��6=   Z� ��6T   Z� .��5(      F�   	minusIcon#��[ 6   $ Uniform=�65   
�������6V   5)      �� 5*   
   -
   5         ��Name$ menuIcon��5*      $��6,2,548,368q�6f   ��6#   Z� ��6:   Z� ��6P   Z� 6f   �� 5+   
   -   5         ��Name$ nameTextBox��5+      $�� ��6%   $
��Left=�60   $��	10,26,0,0q�6K   $	��Top=�6^    J� RenderTransformOrigin$ -0.737,-0.346&�6v   $	��183��6�   ���	#FF62FF19��6�   Z� 6�   �� 5,   
   -   5        $ statusTextLabel��5,      $y�Hidden=�6)   $��Status��6=   $
��Left=�6N   $��
379,26,0,0q�6i   $	��Top=�6}   $ -0.737,-0.346&�6�   $��58��6�   ���	#FF62FF19��6�   Z� 6�   �� 5-   
   -   5         ��Name$ comboStatus��5-      $y�Hidden=�6(   $
��Left=�6<   $��
437,25,0,0q�6W   $	��Top=�6k   $	��120��6�   $��27��6�   
���   ���6�   Z� ��6�   Z� 6�   5.      5/      