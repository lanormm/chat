   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DeskChat.userDeskChat.user  5          DeskChat.user.UserChatWi   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:DeskChat.user     5      $��263.045��5      $��430.489��6     N� Title$  	user_chat��5	      .��5      � .+�5   
   �� 
�������$
��Left=�6!   $��29��6<   $	��Top=�6H   $	��430��6`   �� 5   
   -   5         ��Name$ chatName��5      $
��Left=�6B   $��16,0,0,0q�6]   $	��Top=�6o   $	��386��6�   $��29��6�   $
��Bold"�6�   ��6"   �� *Model.Alias+6�   �� 5   
   -   5         ��Name$ chatMessagesList��5   :   $
��Left=�6T   $	��198��6o   $��0,28,0,0q�6|   $	��Top=�6�   $	��430��6�   ��6   �� *Model.Chat.col+ �� View 5      �� 5         chatListStyle! �� ColumnHeaderContainerStyle# [  6   .P�5      ��  �� Width$	 200��6%    �� CellTemplate 5      �� 5      .R�5   "   �� ��6,   �� 5      5      5      5      �� 5   
   -   5         ��Name$ chatMessageTextBox��5      " �� VerticalScrollBarVisibility$
 Auto=�6.   	 �� AcceptsReturn	 . $
��Left=�6f   $��37��6�   $��	0,226,0,0q�6�   
 �� TextWrapping$

 Wrap=�6�   $	��Top=�6�   $	��391��6�   �� 5   
   -   5         ��Name$ btnSendUser��5      $
��Left=�6&   $��391,226,0,0q�6A   $	��Top=�6V   $��39��6n   $��37��6y   ���	#FF102D57��6�   Z� ��6�   Z� .��5      F�   send#��[ 6    �� Stretch$ Uniform=�60   
�������$��22.621��6T   $��20.558��6d   6u   5      5       5!      