   M S B A M L   `   `   `  ����  Ozyaysan!    Ozyaysan.Windows.W_UseStock   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.31016.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Stoğu Kullan��5   	   $	��229��6   $	��440��6+    � 
ResizeMode$ CanMinimize=�67    � WindowStartupLocation$ CenterOwner=�6P    � 	Resources 5         MyButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� Padding	 �� CornerRadius
 �� Triggers P� Property P� Value �� 
TargetName��%  &	        �� 5   
   " ��6&   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5	       � ��$ 5q�6'   �� 5
       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   $��1q�5      $	 4,2q�5      
�������$	 3��5      #��z��5      ._�5      �� $��Center=�6/   $��Center=�6L   5      
 5      P� 5       �@J�IsMouseOver . . �5   "   �� $ border��6)    � ��
 �   �6T   5      5      5      5      5      .��5      � .+�5    
   �� -   5        $��
textBlock1��5    N   $��23��6   $
��Left=�6    $��	48,22,0,0q�6;   $��Stoktaki Adet��6`   $	��Top=�6u   $	��108��6�   �� 5!   
   -   5        $��lblStock��5!   [   $��Label��6   $��28��6    $
��Left=�6,   $��
232,16,0,0q�6G   $	��Top=�6k   $
��Bold"�6�   �� 5"   
   -   5        $��
textBlock2��5"   N   $��23��6   $
��Left=�6    $��	48,60,0,0q�6;   $��Üretilecek Adet��6`   $	��Top=�6w   $��83��6�   �� 5#   
   -   5        $��
textBlock3��5#   O   $��23��6   $
��Left=�6    $��
205,21,0,0q�6;   $��:��6a   $	��Top=�6j   $��11��6�   �� 5$   
   -   5        $��
textBlock4��5$   O   $��23��6   $
��Left=�6    $��
205,57,0,0q�6;   $��:��6a   $	��Top=�6j   $��11��6�   �� 5%   
   -   5        $��lblStotalCount��5%   o   $��Label��6   $��Normal"�6    $��28��64   $
��Left=�6@   $��
232,52,0,0q�6[   $	��Top=�6�   �� 5&   
   -   5        $��
textBlock5��5&   N   $��23��6   $
��Left=�6    $��	48,94,0,0q�6;   $"��Stoktan Kullanılacak Miktar��6`   $	��Top=�6�   $	��151��6�   �� 5'   
   -   5        $��
textBlock6��5'   O   $��23��6   $
��Left=�6    $��
205,94,0,0q�6;   $��:��6a   $	��Top=�6j   $��11��6�   �� 5(   
   -	   5        $��txtUseOfStock��5(   M   $��23��6   $
��Left=�6   $��
232,93,0,0q�69   $	��Top=�6b   $	��137��6z   6�   �� 5)   
   -
   5        $��btnSave��5)   ^   $��Kaydet��6   $��28��6"   $
��Left=�6.   $��232,136,0,0q�6I   #��[  6m   $	��Top=�6�   $	��106��6�   ��5*      �� 5+       �� ImageSource$4 ./Ozyaysan;component/images/Icon/btn_Update.png��6    j� Stretch$
 Fill=�6Z    j� TileMode$
 None=�6i   5-      5.      5/      