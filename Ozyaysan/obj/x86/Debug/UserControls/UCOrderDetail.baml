   M S B A M L   `   `   `  ����C  ?Ozyaysan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Ozyaysan.ControlsOzyaysan.Controls  5      )    #Ozyaysan.UserControls.UCOrderDetail   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.31016.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /ctrclr-namespace:Ozyaysan.Controls     5      6my-http://schemas.microsoft.com/wpf/2008/toolkit  5   7     � 	Resources  5	         MyButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� CornerRadius �� Triggers P� Property	 P� Value
 �� 
TargetName��%  &	        �� 5   
   " ��6&   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       � ��$ 5q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   $��1q�5      $	 4,2q�5      
�������$ 3��5      #��z��5      ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver	 . . �5   "   �� $
 border��6)    � ��
 �   �6T   5      5      5       5!      5"      .��5#      � $��Stretch=�6    � ColumnDefinitions 5$   
   �� 5%      $��2* �6   6,   �� 5&      $��2* �6   6,   �� 5'      $��2* �6   6,   �� 5(      $��2* �6   6,   �� 5)      $��3* �6   6,   �� 5*      $��2* �6   6,   �� 5+      $��2* �6   6,    � RowDefinitions 5-   
   �� 5.      $��60 �6   6*   �� 5/      $��60 �6   6*   �� 50      $��60 �6   6*   �� 51      $��30 �6   6*   �� 52      $��* �6   6)   �� 53      $��60 �6   6*   .+�55   
   �� -   5         ��Name$ 
lblOrderNo��55   o   $��0��6   $��0��6   $��6��6-   $
��Bold"�6A   $
��Left=�6S   $��OrderNo��6�   $��Center=�6�   $��16(�6�   �� 56   
   -   5         ��Name$ lblOrderNoDesc��56   e   $��0��6   $��0��6!   $��6��61   $��Stretch=�6F   $,��&numaralı siparişi incelemektesiniz..��6}   $��Center=�6�   $��	130,0,0,0q�6�   $��16(�6�   �� 57   
   -   5        $��label1��57   \   $��1��6   $��0��6   $��Musteri��6-   $
��Left=�6@   $��Center=�6j   $��16(�6�   �� 58   
   -   5        $��cmbCustomer��58   M   $��1��6   $��1��6    $	��150��60   $��30��6?   $��Center=�6`   $
��Left=�6{   $��16(�6�   �� 59   
   -   5        $��label12��59      $��1��6   $��2��6   $��Center=�6-   $��Center=�6H   $��Siparis Durumu��6e   $��16(�6�   $��16.158,16,16.158,10q�6�   �� 5:   
   -   5        $��cmbOrderStatus��5:   <   $��1��6   $��3��6    $��30��60   $��	ExtraBold"�6S   $��Center=�6k   $��Stretch=�6�   $��16(�6�   .@�5;      �� -   5        $
��cbi1��5;   /   $��	Beklemede��6   
 � Tag$ 1��6;   �� 5<      -   5        $
��cbi2��5<   .   $��	İmalatta��6   $ 2��6:   �� 5=      -	   5        $
��cbi3��5=   0   $��
Boyahanede��6   $ 3��6<   �� 5>      -
   5        $
��cbi4��5>   /   $��
Boyanıyor��6   $ 4��6;   �� 5?      -   5        $
��cbi5��5?   +   $��Hazır��6   $ 5��67   �� 5@      -   5        $
��cbi6��5@   0   $��Gönderildi��6   $ 6��6<   5A      �� 5B   
   -   5        $��label6��5B   n   $��1��6   $��4��6   $�� Tahmini üretim süresi��6-   $��Stretch=�6O   $��Center=�6|   $��16(�6�   $��60,0,0,0q�6�   �� 5C   
   -   5        $��lblTotalProductionTime��5C   a   $��1��6   $��5��6   $��0��6-   $��28��69   $
��Left=�6E   $��Center=�6   $��Black"�6�   $��50��6�   $
��Left=�6�   $��16(�6�   �� 5D   
   $��2��6   $��0��6   $��Teslimat Tarihi��6-   $
��Left=�6H   $��Center=�6c   $��16(�6~   +  %Microsoft.Windows.Controls.DatePicker  5E   
   -   5        $��dpDateOfDelivery��5E   k   $��2��6   $��1��6%   $��Center=�65   $
��Left=�6P   $	��150��6�   $��30��6�   $��16(�6�   6�   � 5F   
   -   5        $��brdRightContentTitle��5F   A   $��3��6   $��0��6   $��7��6,   ���	#FFE2DFDF$��25��6u   $��Stretch=�6�   $��Center=�6�   $y�Visible=�6�   .+�5G      �� -   5        $ 
textBlock1��5G   A   $��Center=�6   $��23��65   $��16(�6V   �� 6f   $�Siparişi Verilen Yay��6j   �� 6�   $	�lar��6�   6�   �� 5H      -   5         ��Name$ 	imgDelete��5H   f   $��15��6   $��Right=�6"   $
��Fill=�6y   $��Center=�6�   $��15��6�   $0��*/Ozyaysan;component/images/Icon/delete.png��6�   $��0,0,30,0q�6�   �� 5I      -   5        $��imgAddProduct��5I   O   $��15��6   $��Right=�6!   $��0,0,5,0q�6=   $
��Fill=�6d   $��Center=�6s   $��15��6�   $-��'/Ozyaysan;component/images/Icon/add.png��6�   6�   5J      )  #Microsoft.Windows.Controls.DataGrid  5K   
   -   5          Name$ dgOrderProduct��5K   �   $��4��6   $��0��6#   $��7��63   $��Stretch=�6H      AutoGenerateColumns .  $	��Top=�6�   $	��267��6�   $��16(�6�      CanUserAddRows .     Columns 5L      7  1Microsoft.Windows.Controls.DataGridTemplateColumn  5M         CellTemplate 5N      �� 5O      .R�5P      �� -   5        $��Incele��5P   c   ��6(   �� *ID+6c   5Q      5S      3  -Microsoft.Windows.Controls.DataGridTextColumn  5T      /  )Microsoft.Windows.Controls.DataGridColumn   Header$ Yay Kodu��5U   /      Width8  2Microsoft.Windows.Controls.DataGridLengthConverter$ * 6A   4  .Microsoft.Windows.Controls.DataGridBoundColumn   Binding 6   �� *Code+6A     5V      $ 	Renk Kodu��5W   6   $ * 6I    6   �� *CColor.Name+6I     5a      $ Sipariş adeti��5b   4   $ * 6K    6   �� *
OrderCount+6K     5c      $ Stoktan Kul. Adet(Renkli)��5d   ?   $ * 6b    6   �� *UsedStockCountColored+6b     5e      $  Stoktan Kul. Adet(Renksiz)��5f   B   $ * 6f    6   �� *UsedStockCountNonColored+6f     5g      $ Üretilecek Adet��5h   9   $ * 6R    6   �� *ProductionCount+6R   5j      �� 5l   
   -   5        $��	btnSearch��5l   �   $��16(�6   $��5��6D   $��   Güncelle��6Q   $��0��6g   $��36��6w   $
��Left=�6�   #��[  6�   $��Center=�6�   $	��126��6�   $��	5,12,0,12q�6  ��6   �� *ShowPanelCommand+��5m      �� 5n       �� ImageSource$4 ./Ozyaysan;component/images/Icon/btn_Update.png��6    j� Stretch$
 Fill=�6Z    j� TileMode$
 None=�6i   5q      5s      5t      