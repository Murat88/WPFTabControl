   M S B A M L   `   `   `  ����  Ozyaysan    Ozyaysan.Login   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.31016.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   M   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�     � Title$  Giriş��6	    � WindowStartupLocation$ CenterScreen=�6   $	��204��6�   $	��501��6�    � 
ResizeMode$ CanMinimize=�6�    � 	Resources 5         MyButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� Padding	 �� CornerRadius
 �� Triggers P� Property P� Value �� 
TargetName��%  &	        �� 5   
   " ��6&   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5	       � ��$ 5q�6'   �� 5
       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   $��1q�5      $	 4,2q�5      
�������$	 3��5      #��z��5      ._�5      �� $��Center=�6/   $��Center=�6L   5      
 5      P� 5       �@J�IsMouseOver . . �5   "   �� $ border��6)    � ��
 �   �6T   5      5      5      5      5      .��5      � $	��155��6   $	��474��6   .+�5    
   �� -   5        $��label1��5    c   $��Kullanıcı Adı��6   $��28��6(   $
��Left=�64   $��
145,31,0,0q�6O   $	��Top=�6q   �� 5!   
   -   5        $��label2��5!   [   $��Şifre��6   $��28��6    $
��Left=�6,   $��
148,66,0,0q�6G   $	��Top=�6i   �� 5"   
   -   5        $��txtUserName��5"   M   $��23��6   $
��Left=�6   $��
232,35,0,0q�69   $	��Top=�6`   $	��196��6x   $��Muhasebe��6�   �� 5#   
   -   5        $��btnLogin��5#   L   $��30��6   $
��Left=�6   $��
230,98,0,0q�68   #��[  6\   $	��Top=�6�   $	��108��6�   $��Giriş��6�   ��5$      �� 5%       �� ImageSource$4 ./Ozyaysan;component/images/Icon/btn_Update.png��6    j� Stretch$
 Fill=�6Z    j� TileMode$
 None=�6i   5'      G� 5(   
   -   5        $��PwdLogin��5(   5   $��23��6   $��	0,68,46,0q�6"   $	��Top=�6E   $��Right=�6]   $	��196��6y   �� 5)   
   -   5        $��image1��5)   J   $��80��6   $
��Left=�6   $��	26,27,0,0q�67   $
��Fill=�6X   $	��Top=�6g   $	��113��6   $-��'/Ozyaysan;component/images/image001.jpg��6�   5*      5+      