   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:DeskChat.groupDeskChat.group  5          DeskChat.group.GroupChat   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:DeskChat.group     5      $��263.045��5      $��430.489��6     N� Title$  
group_chat��5	      .��5      � .+�5   
   �� -   5         ��Name$ textBox1��5      $
��Left=�6$   $��31��6?   $��	0,232,0,0q�6K    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��391��6�   �� 5   
   -   5        $
��Left=�5      $��391,232,-1,-1q�6,   $	��Top=�6C   $��39��6[   ���	#FF102D57$��31��6�   ��6f   Z� ��6}   Z� .��5      F�    send#��[  6    �� Stretch$ Uniform=�60   
�������$��22.621��6T   $��20.558��6d   6u   5      �� 5   
   -   5         ��Name$ usersConversationView��5      $
��Left=�62   $	��203��6M   $��
315,29,0,0q�6Z   $	��Top=�6n   $	��115��6�   ��5      �� 5       �� 
TargetType" ��6   .�5      -   5        5       �� View 5      �� 5      $��Visible=�6   $��Visible=�6N     chatListStyle! �� ColumnHeaderContainerStyle# [ 6�   .P�5      ��  �� CellTemplate 5      �� 5      .R�5   "   A�   StatusStyle#��[ 6*   $
��Left=�6O   $��10��6j   $	��Top=�6v   $��10��6�   5      5      �� 5        5!      �� 5"      .R�5#   "   �� ��6,   �� *Alias+5$      5&      5'      5)      �� 5*   
   -   5        $ chats��5*      $
��Left=�6"   $	��203��6=   $	��Top=�6J   $	��315��6b   $��0,29,0,0q�6n    5+      �� 5,      $��Visible=�6   $��Visible=�6N   # [ 6�   .P�5-      �� 	 �� Width$		 200��6%    5.      �� 5/      .R�50   "   �� ��6,   �� 51      53      54      56      �� 57   
   -   5        
 ��Name$
 chatName��57      $
��Left=�6A   $��16,0,0,0q�6\   $	��Top=�6n   $	��386��6�   $��29��6�   $
��Bold"�6�   ��6"   �� *
room.Alias+6�   59      5:      