   M S B A M L   `   `   `  ����  Ozyaysan)    #Ozyaysan.UserControls.UCSearchOrder   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.31016.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6my-http://schemas.microsoft.com/wpf/2008/toolkit  5        � 	Resources  5	         MyButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� CornerRadius �� Triggers P� Property	 P� Value
 �� 
TargetName��%  &	        �� 5   
   " ��6&   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       � ��$ 5q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   $��1q�5      $	 4,2q�5      
�������$ 3��5      #��z��5      ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver	 . . �5   "   �� $
 border��6)    � ��
 �   �6T   5      5      5       5!      5"      .��5#      �  � ColumnDefinitions 5$   
   �� 5%      $��* �6   6+   �� 5&      $��* �6   6+   �� 5'      $��* �6   6+   �� 5(      $��* �6   6+    � RowDefinitions 5*   
   �� 5+      $��50 �6   6*   �� 5,      $��50 �6   6*   �� 5-      $��50 �6   6*   �� 5.      $	��350��6   $��* �6,   69   �� 5/      $��50 �6   6*   .+�51   
   �� -   5        $��label3��51   k   $��0��6   $��0��6   $��Sipariş No��6.   $��35��6C   $
��Left=�6O   $��Center=�6y   $��16(�6�   �� 52   
   -   5        $��
txtOrderNo��52   0   $��0��6   $��0��6    $��Stretch=�6B   $��80,0,0,0q�6`   $��30��6r   $	��150��6~   $��16(�6�   6�   �� 53   
   -   5        $��label32��53   w   $��0��6   $��1��6   $��2��6-   $	��Yay��6A   $��35��6O   $
��Left=�6[   $��Center=�6�   $��16(�6�   ]� 54   
   $��0��6   $��1��6!   $��Stretch=�61   $��50,0,0,0q�6O   .+�55      �� -   5        $��
cmbProduct��55   %   $��30��6   $��Stretch=�67   $	��150��6U   $��Center=�6d   $��16(�6   �� 56      -   5        $��imgProductSearch��56   <   $��15��6   $
��Left=�6    $-��'/Ozyaysan;component/images/Icon/ara.png��6T   $��Center=�6�   $��10,0,0,0q�6�   6�   57      �� 58   
   -   5        $��label1��58   f   $��0��6   $��2��6   $��	Müşteri��6-   $��35��6?   $
��Left=�6K   $��Center=�6t   $��16(�6�   �� 59   
   -   5        $��cmbCustomer��59   �   $��0��6   $��2��6!   $��30,0,0,0q�62   $��30��6E   $��Stretch=�6Q   $	��150��6o   $��Center=�6�   $��16(�6�   �� 5:   
   -   5        $��label12��5:   n   $��0��6   $��3��6   $��Sipariş Durumu��6-   $��35��6F   $
��Left=�6R   $��Center=�6}   $��16(�6�   �� 5;   
   -	   5        $��cmbOrderStatus��5;   }   $��0��6   $��3��6    $��90,0,0,0q�61   $��30��6C   $��Stretch=�6O   $	��150��6m   $��Center=�6�   $��16(�6�   .@�5<      �� -
   5        $
��cbi0��5<      
 � Tag$ 0��6(   .��60   Tümü66   �� 5=      -   5        $
��cbi1��5=      $ 1��6(   .��60   İmalata Hazır6?   �� 5>      -   5        $
��cbi2��5>      $ 2��6(   .��60   
Imalatta6:   �� 5?      -   5        $
��cbi3��5?      $ 3��6(   .��60   
Boyahanede6<   �� 5@      -   5        $
��cbi4��5@      $ 4��6(   .��60   
Boyanıyor6;   �� 5A      -   5        $
��cbi5��5A      $ 5��6(   .��60   Hazır67   �� 5B      -   5        $
��cbi6��5B      $ 6��6(   .��60   Gönderildi6<   5C      �� 5D   
   -   5        $��label11��5D   �   $��1��6   $��0��6   $��2��6-   $��Teslimat Tarihi��6A   $��28��6[   $
��Left=�6g   $��Center=�6�   $��16(�6�   �� 5E   
   $��1��6   $��0��6"   $��	150,0,0,0q�62   $��2��6E    �� Orientation$ 
Horizontal=�6Y   .+�5F      +  %Microsoft.Windows.Controls.DatePicker  -   5        $��dpDateOfDeliveryFirst��5F   D   $
��Left=�6   $��30��67   $��Center=�6a   $	��118��6|      Text$ Başlangıç��6�   �� 5G      -   5        $��label2��5G   I   $��--��6   $��28��6!   $
��Left=�6-   $��Center=�6W     5H      -   5        $��dpDateOfDeliverySecond��5H   D   $
��Left=�6   $��30��67   $��Center=�6b   $	��118��6}   5I      �� 5J   
   -   5        $��	btnSearch��5J   V   $��2��6   $��0��6   $��36��6.   $
��Left=�6:   #��[  6g   $��Center=�6�   $	��132��6�   ��5K      �� 5L       �� ImageSource$4 ./Ozyaysan;component/images/Icon/btn_Update.png��6    j� Stretch$
 Fill=�6Z    j� TileMode$
 None=�6i   5N      �� 5O   
   -   5        $��
textBlock1��5O   y   $��2��6   $��0��6!   $��16(�61   $��23��6?   $��60,0,0,0q�6K   $
��Left=�6]   $	��Ara��6�   $��Center=�6�   )  #Microsoft.Windows.Controls.DataGrid  5P   
   -   5        $��dgOrders��5P   �   $��3��6   $��0��6$   $��7��64      AutoGenerateColumns .     CanUserAddRows .  $��Stretch=�6|   $	��Top=�6�   $��16(�6�      Columns 5Q      7  1Microsoft.Windows.Controls.DataGridTemplateColumn  5R         CellTemplate 5S      �� 5T      .R�5U      �� -   5        $��Incele��5U   [   $��80��6l   ��6:   �� *OID+6l   5V      5X      3  -Microsoft.Windows.Controls.DataGridTextColumn  5Y      /  )Microsoft.Windows.Controls.DataGridColumn   Header$ Sipariş No��6I      Width8  2Microsoft.Windows.Controls.DataGridLengthConverter$ * 6]   4  .Microsoft.Windows.Controls.DataGridBoundColumn   Binding 6(   ��  �� Path$ OrderNo�6]     5Z      $ Teslimat Tarihi��6n   $ * 6�    6(   �� $ DateOfDelivery� �� StringFormat$ {0:dd/MM/yyyy}��6�     5[      $ 	Müşteri��6G   $ * 6X    6(   �� $ CName�6X     5\      $ Sipariş Durumu��6H   $ * 6`    6(   �� $ OSName�6`     5]      $$ Tahmini Üretim Süresi (Gün)��6^   $ * 6�    6(   �� $" EstimatedTotalProductionTime�6�   5`      �� 5a   
   -   5        $��stackPanel1��5a   o   $��4��6   $��0��6#   $��4��63   $��29��6G   $
��Left=�6S   $��Center=�6�   $ 
Horizontal=�6�   .+�5b      �� -   5        $��btnAlPrevious��5b   4   $��<<��6   $��23��6(   $��22��6I   6T   �� 5c      -   5        $��btn1Previous��5c   0   $��<��6   $��23��6$   $��17��6D   6O   �� 5e      -   5        $��txtPage��5e   "   $��23��6   $��30��61   $��0��6<    �� TextAlignment$ Center=�6E   {�.  $
��Bold"�6n   �� 5f      -   5        $��btn1Forward��5f   0   $��>��6   $��23��6$   $��17��6C   6N   �� 5g      -   5        $��btnAllForward��5g   4   $��>>��6   $��23��6(   $��22��6I   6T   5h      �� 5i   
   -   5        $��stackPanel21��5i   o   $��4��6   $��0��6#   $��4��63   $��29��6G   $
��Left=�6S   $��Center=�6�   $��	120,0,0,0q�6�   $ 
Horizontal=�6�   .+�5j      �� -    5        $��lblRecordCount��5j   }   $��0��6   $��Tahoma)�6!   $��12(�65   $
��Bold"�6C   $��28��6V   $
��Left=�6b   6}   �� 5k      -!   5        $��label4��5k   z   $�� kayıt bulundu ��6   $��Tahoma)�6/   $��12(�6C   $��28��6R   $
��Left=�6^   6z   5l      5m      5n      