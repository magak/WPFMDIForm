   M S B A M L   `   `   `  ����  
WPFMDIForm    WPFMDIForm.PageFlats   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          � 	Resources  5       
  Button �� 
TargetType �� Property �� Value��%  &	        �� 5	   
   " ��6   .�5
      ��  �@��Right$ 10��6-   �� 5       � ��
 ��ح�6+   �� 5       � ��
 �   �6+   �� 5       � ��$ 20��6'   �� 5       � ��$	 100��6&   �� 5       � ��$ Center=�6;   5      5      .��5      � 
�������$
��Left=�5      $	��Top=�6&   .+�5   
   �� -   5        $��label1��5   2   $��32��6   $��0,-2,536,530q�6   $��Center=�6@   $��Center=�6[   $��Stretch=�6}   $��15(�6�   $��SemiBold"�6�   .��6�   Квартиры5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgvTable��5         AutoGenerateColumns .  $��0,65,0,0q�5   /    �� SelectedItem 6A   �� *SelectedFlat+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Дом��5      1  +System.Windows.Controls.DataGridBoundColumn   Binding 5      �� *Дом.Адрес+  5      $! Номер Квартиры��5        5!      �� *Номер_квартиры+  5#      $% Площадь квартиры��5$       5%      �� *!Площадь_квартиры+5(      �� 5)   
   -   5        $��btnAdd��5)      $��	10,36,0,0q�5*      $��Добавить��5,      	 J� RenderTransformOrigin$	 
1.1,-0.045&�6$   $
��Left=�6G   $��87��6b   $��22��6m   $	��Top=�6y   �� 5.   
   -   5        $��btnUpd��5.      $��
114,36,0,0q�5/      $ ��Редактировать��51      $	 -0.069,0.636&�6)   $
��Left=�6N   $��87��6i   $��22��6t   $	��Top=�6�   �� 53   
   -   5        $��btnDel��53      $��
218,36,0,0q�54      $��Удалить��55      $
��Left=�68   $��50��6S   $��22��6^   $	��Top=�6j   57      58      