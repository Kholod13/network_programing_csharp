   M S B A M L   `   `   `  ����E  AMailSender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MailSender
MailSender  5   	       MailSender.MailInteface   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:MailSender     5   	     � Title$  MailInteface��5   	   $	��550��6   $
��1000��6+   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��11* �6   6-   �� 5      $	��18* �6   6-   �� 5      $	��11* �6   6-    � RowDefinitions 5   
   �� 5      $	��63* �6   6+   �� 5      $
��320* �6   6,   �� 5      $	��51* �6   6+   .+�5   
   �� -   5        $��2��5      $��1��6   $��18(�6/   $��1q�6R   $��84,11,84,10q�6f   .��6}   Exit6�   �� 5   
   -   5        $��0��5      $��2��6   $��18(�6E   $��1q�6S   $��43,17,44,16q�6g   .��6}   Send Message6�   �� 5   
   $��1��6   $��Center=�6$   $��Center=�6A   $��36(�6\   $��	UltraBold"�6j   $��Oblique$�6�   
����  �$��48��6�   $	��211��6�   
MailSender6�   �� 5   
   -   5         ��Name$ 
FolderList��5   ^   $��1��6A   $��0��6N   $��10q�6r   6�   �� 5   
   -   5        $ MessagesList��5   `   $��1��6C   $��1��6P   $��10q�6v   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value$
 30px��62   �� 5        � ��$	 1pxq�68   5!      5$      �� 5%   
   $��1��6   $��3��6"   $
��20pxq�62   .+�5&      �� -   5         ��Name$ inputTxtBox��5&      $
��30px��6+   $	��1pxq�69   $
��18px(�6O   $��Center=�6_    �� TextAlignment$ Center=�6z   $	��5pxq�6�   6�   A� 5'      .+�5(      �� -   5        	 ��Name$	 AddToFolderBtn��5(   6   $	��2pxq�6N   $
��30px��6[   .��6i   Add to folder6x   �� 5)      -   5        $	 CreateNewFolderBtn��5)   :   $	��2pxq�6V   $
��30px��6c   .��6q   Create new Folder6�   �� 5*      -   5        $	 RenameFolderBtn��5*   7   $	��2pxq�6P   $
��30px��6]   .��6k   Rename Folder6z   �� 5+      -	   5        $	 DeleteFolderBtn��5+   7   $	��2pxq�6P   $
��30px��6]   .��6k   Delete Folder6z   5,      �� 5-      .+�5.      ]� .+�5/      �� $
��Left=�6   .��67   Date:6>   �� 50      -
   5        
 ��Name$
 DateTxtBlock��50       $��Center=�66   6S   51      ]� 52      .+�53      �� $��Center=�6   .��69   
Subject:6C   �� 54      -   5        $
 SubjectTxtBlock��54       $��Center=�69   6V   55      �� 56      $��Center=�6   $
��14px(�65   .��6E   	Message6N   �� 57      -   5        $
 MessageTxtBlock��57      67   58      59      5<      5=      