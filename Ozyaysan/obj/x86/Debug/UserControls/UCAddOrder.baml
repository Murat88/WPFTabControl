   M S B A M L   `   `   `  ����  Ozyaysan&     Ozyaysan.UserControls.UCAddOrder   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.31016.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6my-http://schemas.microsoft.com/wpf/2008/toolkit  5      $
��auto��6G     � 	Resources  5         MyButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� CornerRadius �� Triggers P� Property	 P� Value
 �� 
TargetName��%  &	        �� 5
   
   " ��6&   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       � ��$ 5q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   $��1q�5      $	 4,2q�5      
�������$ 3��5      #��z��5      ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver	 . . �5   "   �� $
 border��6)    � ��
 �   �6T   5      5      5      5       5!      .��5"      � $��Stretch=�6    � ColumnDefinitions 5#   
   �� 5$      $��* �6   6+   �� 5%      $��* �6   6+   �� 5&      $��* �6   6+   �� 5'      $��* �6   6+   �� 5(      $��* �6   6+    � RowDefinitions 5*   
   �� 5+      $��60 �6   6*   �� 5,      $��60 �6   6*   �� 5-      $��60 �6   6*   �� 5.      $��30 �6   6*   �� 5/      $��* �6   6)   �� 50      $��60 �6   6*   .+�52   
   �� -   5        $��label1��52   �   $��0��6   $��0��6   $��Musteri��6-   $��28��6?   $
��Left=�6K   $��Center=�6g   $��16(�6�   �� 53   
   -   5        $��cmbCustomer��53   [   $��0��6   $��1��6    $��30��60   $��Stretch=�6<   $��Center=�6n   $��16(�6�   �� 54   
   -   5        $��label6��54   z   $��1��6   $��0��6   $!��Tahmini Uretim Suresi(gün)��6-   $��44��6R   $
��Left=�6^   $��Center=�6�   $��16(�6�   $��210.8��6�   $��	0,26,0,10q�6�   �� 55   
   -   5        $��lblProductionTime��55   u   $��1��6   $��1��6   $��28��6-   $
��Bold"�69   $��0��6K   $��Center=�6W   $��Center=�6�   $��16(�6�   �� 56   
   -   5        $��
textBlock2��56   Q   $��1��6   $��2��6!   $��Stretch=�62   $]��W(Bu veri teslimat tarihini belirlemede yol gösterici olması için hesaplanmıştır.)��6c   $��Center=�6�   $
��Wrap=�6�   $��14(�6�   �� 57   
   -   5        $��label11��57   o   $��2��6   $��0��6   $��Teslimat Tarihi��6-   $��28��6G   $
��Left=�6S   $��Center=�6~   $��16(�6�   +  %Microsoft.Windows.Controls.DatePicker  58   
   -   5        $��dpDateOfDelivery��58   b   $��2��6   $��1��6%   $��16(�65   $��Stretch=�6C   $��Center=�6z   � 59   
   -   5        $��brdRightContentTitle��59   A   $��3��6   $��0��6   $��5��6,   ���	#FFE2DFDF$��25��6u   $��Stretch=�6�   $��Center=�6�   $y�Visible=�6�   .+�5:      �� -	   5         ��Name$ 
textBlock1��5:   A   $��Center=�6   $��23��65   $��16(�6V   �� 6f   $�Siparişi Verilen Yay��6j   �� 6�   $	�lar��6�   6�   �� 5;      -
   5         ��Name$ 	imgDelete��5;   f   $��15��6   $��Right=�6"   $
��Fill=�6y   $��Center=�6�   $��15��6�   $0��*/Ozyaysan;component/images/Icon/delete.png��6�   $��0,0,30,0q�6�   �� 5<      -   5        $��imgAddProduct��5<   O   $��15��6   $��Right=�6!   $��0,0,5,0q�6=   $
��Fill=�6d   $��Center=�6s   $��15��6�   $-��'/Ozyaysan;component/images/Icon/add.png��6�   6�   5=      )  #Microsoft.Windows.Controls.DataGrid  5B   
   -   5          Name$ dgOrderProduct��5B   �   $��4��6   $��0��6#   $��5��63   $��Stretch=�6G      AutoGenerateColumns .  $	��Top=�6�   $
��auto��6�   $��246.116��6�      CanUserAddRows .  $��16(�6�      Columns 5C      7  1Microsoft.Windows.Controls.DataGridTemplateColumn  5D         CellTemplate 5E      �� 5F      .R�5G      �� $��Incele��6H   ��6(   �� *ID+6H   5H      5J      3  -Microsoft.Windows.Controls.DataGridTextColumn  5K      /  )Microsoft.Windows.Controls.DataGridColumn   Header$ Yay Kodu��5L   /      Width8  2Microsoft.Windows.Controls.DataGridLengthConverter$ * 6A   4  .Microsoft.Windows.Controls.DataGridBoundColumn   Binding 6   �� *Code+6A     5N      $
 Renk��6,   $ * 6:    �� 
IsSelected .   5O      �� 5P      .R�5Q      �� ��6&   �� *CColor.RGBCode+5R      5U        5X      $ Sipariş adeti��5Y   4   $ * 6K    6   �� *
OrderCount+6K     5Z      $ Stoktan Kul. Adet(Renkli)��5[   ?   $ * 6b    6   �� *UsedStockCountColored+6b     5\      $  Stoktan Kul. Adet(Renksiz)��5]   B   $ * 6f    6   �� *UsedStockCountNonColored+6f     5^      $ Üretilecek Adet��5_   9   $ * 6R    6   �� *ProductionCount+6R   5a      �� 5d   
   -   5        $��	btnSearch��5d   h   $��5��6   $��0��6   $��44.941��6/   $��16(�6?   $
��Left=�6M   #��[  6y   $��Center=�6�   $��177.481��6�   ��5e      �� 5f       �� ImageSource$4 ./Ozyaysan;component/images/Icon/btn_Update.png��6    j� Stretch$
 Fill=�6Z    j� TileMode$
 None=�6i   $��Kaydet��5h   4   5i      5j      5k      