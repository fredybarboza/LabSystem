   M S B A M L   `   `   `  ����L  HLaboratorioBogado, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:LaboratorioBogadoLaboratorioBogado  5   	   $    LaboratorioBogado.ImprimirPage   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:LaboratorioBogado     5   	     � Title$  ImprimirPage��5   	   $��772.8��6   $	��800��6-   .��5	      � -   5         �Name$ mainGrid��5	      $��0,0,2,0q�6   .+�5
   
   � -   5        $ impresionGeneralGrid��5
      $
��Left=�6-   $	��710��6H   $��	49,22,0,0q�6U   $	��Top=�6h   $	��579��6�   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ imprimirDataGrid��5      $
��Left=�61   $	��537��6L   $��	0,163,0,0q�6Y   $	��Top=�6l   $	��579��6�      GridLinesVisibility$ 
Horizontal=�6�      HeadersVisibility$
 None=�6�    J� OpacityMask
 �   �   AutoGenerateColumns .     CanUserReorderColumns .     CanUserResizeColumns .  	   HorizontalGridLinesBrush	 �	#FF837D7D��6�   Z� ��6�   Z� 
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6)   -   5        	param��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   D   �� *	Parametro+6b     5      $	 100 6)    .  65   �� *Valor+6O     5      $	 100 6)    .  65   �� *Unidad+6P     5      $	 200 6)    .  65   �� *ValorReferencia+6Y   5      �� 5      $��LABORATORIO��6   $
��Left=�6*   $��
213,49,0,0q�6E   $	��Top=�6Y   $��16(�6q   $
��Bold"�6   �� 5      $*��$HOSPITAL DISTRITAL DE CORONEL BOGADO��6   $
��Left=�6C   $��	49,10,0,0q�6^   $	��Top=�6q   $��22(�6�   $
��Bold"�6�   �� 5      $��	Paciente:��6   $
��Left=�6(   $��	49,89,0,0q�6C   $	��Top=�6V   $
��Bold"�6n   �� 5      $
��C.I:��6   $
��Left=�6#   $��
49,115,0,0q�6>   $	��Top=�6R   $
��Bold"�6j   �� 5      $��Edad:��6   $
��Left=�6$   $��213,115,0,0q�6?   $	��Top=�6T   $
��Bold"�6l   �� 5      $��Fecha:��6   $
��Left=�6%   $��366,115,0,0q�6@   $	��Top=�6U   $
��Bold"�6m   �� 5      $��	Servicio:��6   $
��Left=�6(   $��
366,89,0,0q�6C   $	��Top=�6W   $
��Bold"�6o   �� 5      $
��Left=�6   $��26��63   $��	0,132,0,0q�6?   $	��Top=�6R   $	��579��6j   ���	#FF8985856v   5      �� 5   
   -   5         ��Name$ imprimirButton��5      $��Imprimir��6)   $
��Left=�6<   $��
688,22,0,0q�6W   $	��Top=�6k   $��75��6�   6�   5      5#      