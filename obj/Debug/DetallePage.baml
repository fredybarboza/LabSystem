   M S B A M L   `   `   `  ����L  HLaboratorioBogado, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:LaboratorioBogadoLaboratorioBogado  5   	   #    LaboratorioBogado.DetallePage   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:LaboratorioBogado     5   	     � Title$  Detalle��5   	   $	��400��6   $	��400��6&    � WindowStyle$ 
ToolWindow=�62    � 
ResizeMode$ NoResize=�6K    � WindowStartupLocation$ CenterScreen=�6a   .��5	      � .+�5
   
   �� $
��N °��6   $
��Left=�6   $��
136,10,0,0q�69   $	��Top=�6M   $��22(�6e   $
��Bold"�6s   �� 5   
   $
��Left=�6   $��37��6-   $��
183,14,0,0q�69    �� TextWrapping$
 Wrap=�6M   $��32��6a   $	��Top=�6k   $	��120��6�   $��22(�6�   ��6�   Z� �� 5   
   $��	PACIENTE:��6   $
��Left=�6$   $��	22,53,0,0q�6?   $	��Top=�6R   $��16(�6j   �� 5   
   $
��Left=�6   $��28��6-   $��
109,56,0,0q�69   $
 Wrap=�6M   $��FREDY BARBOZA��6a   $	��Top=�6v   $	��194��6�   $��16(�6�    �� TextAlignment$ Center=�6�   �� 5   
   $
��Left=�6   $	��100��6/   $��0,43,0,0q�6<   $	��Top=�6N   $	��392��6f   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ detallepedidoDataGrid��5      $
��Left=�62   $	��100��6M   $��
91,158,0,0q�6Z   $	��Top=�6n   $	��227��6�      GridLinesVisibility$
 None=�6�      HeadersVisibility$
 None=�6�   ��6�   Z� ��6�   Z� 	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 100 6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Nombre+5      5      5      