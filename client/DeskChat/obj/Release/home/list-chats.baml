   M S B A M L   `   `   `  ���� A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DeskChat.homeDeskChat.home      DeskChat.home.ListChats   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:DeskChat.home       N� Title$  list_groups��$	��290��$	��493��.��� .+��� -    ��Name$ chatsListView��$
��Left=�$	��214��$	��Top=�$	��296��$��	10,29,0,0q�����  �� 
TargetType" ��.�-    �� View ��    chatListStyle! �� ColumnHeaderContainerStyle# [  .P���  �� Width$ 50�� �� CellTemplate �� .R��� $��40��$��40������ *	iconUrl+�� $	 200�� �� .R��� ���� *Alias+�� -   $ usersListView��$
��Left=�$	��214��$��
358,29,0,0q�$	��Top=�$	��111�� � ToolTip$3 -Clique duas vezes para adicionar uma conversa������ " ��.�-    �� # [  .P���  �� .R�A�   StatusStyle#��[ $
��Left=�$��10��$	��Top=�$��10����  �� .R��� ���� *Alias+�� $
��Left=�$��	358,0,0,0q�$
��Wrap=�$��Usuários Logados��$	��Top=�$��29��$	��111�� �� TextAlignment$ Center=�$��SemiCondensed&��� $
��Left=�$
��Wrap=�$��Chats Ativos��$	��Top=�$��29��$	��296��$ Center=�$��SemiCondensed&�$��10,0,0,0q��� -   	 ��Name$	 newGroupBtn��$
��Left=�$��137,262,0,0q�$	��Top=�$��44��
���   �$��20����Z� ��Z� .��F�   newGroup#��[ 
 �� Stretch$
 Uniform=�
���   �
���   ��� -    ��Name$ texboxNewGroup��$
��Left=�$��23��$��
10,260,0,0q� �� TextWrapping$
 Wrap=�$��
Novo Grupo��$	��Top=�$	��120��