   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DeskChat.homeDeskChat.home  5          DeskChat.home.ListChats   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:DeskChat.home     5        N� Title$  list_groups��5      $	��290��6   $	��493��6(   .��5
      � .+�5   
   �� -   5         ��Name$ chatsListView��5      $
��Left=�6*   $	��214��6E   $	��Top=�6R   $	��296��6j   $��	10,29,0,0q�6v   ��5      �� 5       �� 
TargetType" ��6   .�5      -   5        5       �� View 5      �� 5         chatListStyle! �� ColumnHeaderContainerStyle# [  6   .P�5      ��  �� Width$ 50��6%    �� CellTemplate 5      �� 5      .R�5   "   �� $��40��6C   $��40��6N   ��6(   �� *	iconUrl+6N   5      5      �� 5       $	 200��6%    5!      �� 5"      .R�5#   "   �� ��6,   �� *Alias+5$      5&      5'      5)      �� 5*   
   -   5        $ usersListView��5*      $
��Left=�6*   $	��214��6E   $��
358,29,0,0q�6R   $	��Top=�6f   $	��111��6~    � ToolTip$3 -Clique duas vezes para adicionar uma conversa��6�   ��5+      �� 5,      " ��6   .�5.      -   5        5/       51      �� 52      # [  6   .P�53      ��  54      �� 55      .R�56   "   A�   StatusStyle#��[ 6*   $
��Left=�6O   $��10��6j   $	��Top=�6v   $��10��6�   57      59      �� 5:       5;      �� 5<      .R�5=   "   �� ��6,   �� *Alias+5>      5@      5A      5C      �� 5D   
   $
��Left=�6   $��	358,0,0,0q�6/   $
��Wrap=�6B   $��Usuários Logados��6V   $	��Top=�6n   $��29��6�   $	��111��6�    �� TextAlignment$ Center=�6�   $��SemiCondensed&�6�   �� 5E   
   $
��Left=�6   $
��Wrap=�6/   $��Chats Ativos��6C   $	��Top=�6W   $��29��6o   $	��296��6{   $ Center=�6�   $��SemiCondensed&�6�   $��10,0,0,0q�6�   �� 5F   
   -   5        	 ��Name$	 newGroupBtn��5F      $
��Left=�6&   $��137,262,0,0q�6A   $	��Top=�6V   $��44��6n   
���   �$��20��6�   ��6y   Z� ��6�   Z� .��5G      F�   newGroup#��[ 6   
 �� Stretch$
 Uniform=�64   
���   �
���   �6S   5H      �� 5I   
   -   5         ��Name$ texboxNewGroup��5I      $
��Left=�6*   $��23��6E   $��
10,260,0,0q�6Q    �� TextWrapping$
 Wrap=�6e   $��
Novo Grupo��6y   $	��Top=�6�   $	��120��6�   5J      5K      