   M S B A M L   `   `   `  ����  
WPFMDIForm    WPFMDIForm.PageODU   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5        .��5      � 
�������.+�5	   
   �� -   5        $��label1��5	   .   $��32��6   $��0,0,22,0q�6   $	��Top=�6<   $��Stretch=�6T   $��15(�6r   $��SemiBold"�6�   .��6�   PNПоказания общедомового прибора учета воды5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgvTable��5          AutoGenerateColumns  .  $��10,65,10,10q�5   /    �� SelectedItem 6D   �� *SelectedOdu+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Период��5      1  +System.Windows.Controls.DataGridBoundColumn   Binding 5      �� * Календарь.Description+  5      $ ГВС��5       5      �� *Показание_ГВС+  5      $ ХВС��5       5      �� *Показание_ХВС+  5      $ Дом��5       5      �� *Дом.Адрес+5       �� 5!   
   -   5        $��btnAdd��5!      $��	10,32,0,0q�5"      $)��#Добавить показания��5$       J� RenderTransformOrigin$ 
1.1,-0.045&�6.   $
��Left=�6Q   $	��130��6l   $��24��6x   $	��Top=�6�   �� 5&   
   -   5        $��btnUpd��5&      $��
164,33,0,0q�5'      $ ��Редактировать��5)      $ -0.069,0.636&�6)   $
��Left=�6N   $��91��6i   $��24��6t   $	��Top=�6�   �� 5+   
   -   5        $��btnDel��5+      $��
278,33,0,0q�5,      $��Удалить��5.      $
��Left=�6#   $��89��6>   $��23��6I   $	��Top=�6U   50      51      