   M S B A M L   `   `   `  ����  Ozyaysan)    #Ozyaysan.UserControls.UCSearchStock   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.31016.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6my-http://schemas.microsoft.com/wpf/2008/toolkit  5        � 	Resources  5	         MyButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� CornerRadius �� Triggers P� Property	 P� Value
 �� 
TargetName��%  &	        �� 5   
   " ��6&   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       � ��$ 5q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   $��1q�5      $	 4,2q�5      
�������$ 3��5      #��z��5      ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver	 . . �5   "   �� $
 border��6)    � ��
 �   �6T   5      5      5       5!      5"      .��5#      � .+�5%   
   � $	��137��6   $
��Left=�6   $��12,0,0,0q�67   $	��Top=�6J   $
��1076��6b    � ColumnDefinitions 5&      �� 5'      $
��224* �6#   �� 5(      $
��834* �6#   .+�5*      �� -   5        $��label1��5*   [   $	��Yay��6   $��28��6"   $
��Left=�6.   $��6,43,0,0q�6I   $	��Top=�6i   $��77��6�   �� 5+      -   5        $��
cmbProduct��5+   Q   $��23��6   $
��Left=�6#   $��	93,46,0,0q�6>   $	��Top=�6c   $	��188��6{   $��2��6�   �� 5,      -   5        $��border1��5,   �   
�������$��1q�6*   $��2��6>   $��26��6R   $
��Left=�6^   $��5,4,0,0q�6y   $	��Top=�6�   $
��1025��6�   ._�5-      �� -   5        $��label19��5-   �   $��Stok İnceleme Formu��6   $��Tahoma)�66   $��12(�6J   ���	#7B0B8919$��28��6o   $
��Left=�6{   $	��175��6�   5.      �� 5/      -   5        $��	btnSearch��5/   v   $	��Ara��6.   $��31��6<   $
��Left=�6H   $��	93,86,0,0q�6c   #��[  6�   $	��Top=�6�   $	��108��6�   ��50      �� 51       �� ImageSource$4 ./Ozyaysan;component/images/Icon/btn_Update.png��6!    j� Stretch$
 Fill=�6^    j� TileMode$
 None=�6m   53      �� 54      -   5        $��imgProductSearch��54   N   $��15��6   $
��Left=�6    $��	58,50,0,0q�6;   $-��'/Ozyaysan;component/images/Icon/ara.png��6f   $
��Fill=�6�   $	��Top=�6�   $��15��6�   $��1��6�   55      )  #Microsoft.Windows.Controls.DataGrid  57   
   -   5        $��
dgProducts��57   b   $
��Left=�6      AutoGenerateColumns .  $��
12,183,0,0q�6N   $	��Top=�6t   $
��1030��6�   $	��223��6�      CanUserAddRows .     Columns 58      3  -Microsoft.Windows.Controls.DataGridTextColumn  5:      /  )Microsoft.Windows.Controls.DataGridColumn   Header$ Yay Kodu��5;   1      Width8  2Microsoft.Windows.Controls.DataGridLengthConverter$	 300 6C   4  .Microsoft.Windows.Controls.DataGridBoundColumn   Binding 6   ��  �� Path$
 Code�5<        5=      $
 Stok��5>   2   $	 300 6@    6   �� $ Stock�5?        5@      $ Durum��5A   1   $	 380 6@    6   �� $ SName�6@   5E      �� 5F   
   -   5        $��border2��5F   u   
�������$��1q�6&   $��26��6:   $
��Left=�6F   $��
12,150,0,0q�6a   $	��Top=�6�   $
��1025��6�   ._�5G      � .+�5H      �� -	   5        $��label22��5H   �   $�� Yay Listesi (��6   $��Tahoma)�61   $��12(�6E   ���	#7B0B8919$��28��6j   $
��Left=�6v   $	��175��6�   �� 5I      -
   5        $��lblRecordCount��5I   �   $��0��6   $��Tahoma)�6$   $��12(�68   ���	#7B0B8919$��28��6]   $
��Left=�6i   $	��175��6�   $��
101,0,0,-4q�6�   �� 5J      -   5        $��label4��5J   �   $�� kayıt bulundu   )��6   $��Tahoma)�65   $��12(�6I   ���	#7B0B8919$��28��6n   $��Right=�6z   $��0,-1,727,-3q�6�   $	��175��6�   5K      5L      �� 5M   
   -   5        $��stackPanel1��5M   P   $��29��6   $
��Left=�6!   $��
12,409,0,0q�6<   $	��Top=�6c   $
��1030��6{    �� Orientation$ 
Horizontal=�6�   .+�5N      �� -   5        $��btnAlPrevious��5N   4   $��<<��6   $��23��6(   $��22��6I   6T   �� 5O      -   5        $��btn1Previous��5O   0   $��<��6   $��23��6$   $��17��6D   6O   �� 5Q      -   5        $��txtPage��5Q   "   $��23��6   $��30��61   $��0��6<    �� TextAlignment$ Center=�6E   {�.  $
��Bold"�6n   �� 5R      -   5        $��btn1Forward��5R   0   $��>��6   $��23��6$   $��17��6C   6N   �� 5S      -   5        $��btnAllForward��5S   4   $��>>��6   $��23��6(   $��22��6I   6T   5T      5U      5V      