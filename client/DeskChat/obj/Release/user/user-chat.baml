   M S B A M L   `   `   `  ���� A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DeskChat.userDeskChat.user      DeskChat.user.UserChatWi   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:DeskChat.user     $��263.045��$��430.489��  N� Title$  	user_chat��.��� .+��� 
�������$
��Left=�$��29��$	��Top=�$	��430���� -    ��Name$ chatName��$
��Left=�$��16,0,0,0q�$	��Top=�$	��386��$��29��$
��Bold"����� *Model.Alias+�� -    ��Name$ chatMessagesList��$
��Left=�$	��198��$��0,28,0,0q�$	��Top=�$	��430������ *Model.Chat.col+ �� View ��    chatListStyle! �� ColumnHeaderContainerStyle# [  .P���  �� Width$	 200�� �� CellTemplate �� .R��� ���� �� -    ��Name$ chatMessageTextBox��" �� VerticalScrollBarVisibility$
 Auto=�	 �� AcceptsReturn	 . $
��Left=�$��37��$��	0,226,0,0q�
 �� TextWrapping$

 Wrap=�$	��Top=�$	��391���� -    ��Name$ btnSendUser��$
��Left=�$��391,226,0,0q�$	��Top=�$��39��$��37�����	#FF102D57��Z� ��Z� .��F�   send#��[  �� Stretch$ Uniform=�
�������$��22.621��$��20.558��