   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  B >WpfApp6, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:JsonDataDisplayJsonDataDisplay     JsonDataDisplay.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6sys&clr-namespace:System;assembly=mscorlib     /localclr-namespace:JsonDataDisplay       � Title$  XXV-Rate��$
��1000��$
��1900�� � WindowState$ 	Maximized=� � 	Resources  �� 
TargetType �� Property �� Value(  "JsonDataDisplay.IncrementConverter   IncrementConverter��%�   '��       '� E     &	  �     �� " ��.���  � �� �#333333��  � �� �#CCCCCC�� " �.���  � �� �#1E1E1E��  � ��
 �����  .��� $��Stretch=� � ColumnDefinitions �� $��1437* ��� $
��233* ��� $
��230* �.+�r� -    r�Name$ BackgroundVideo�� r� Source$ background.mp4?�	 r� LoadedBehavior$
	 Play=�
 r� IsMuted$

 True�� r� Stretch$ UniformToFill=�$��3��� $��3�� �� $
��Auto ��� $��* � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� -    ��Name$ TeamFilterComboBox��$��0��$��Center=� �� SelectedIndex$ 0��$��42��$��10,10,10,10q�.@��� $��	All Teams���� -   $ PositionFilterComboBox��$��0��$��1��$��Center=�$ 0��$��42��$��10,10,10,10q�.@��� $��All Positions���� -   $ ProgramFilterComboBox��$��0��$��2��$��10,10,10,10q�$��Center=�$ 0��$��42��.@��� $��All Programs���� -   $ ArchetypeFilterComboBox��$��0��$��3��$��10,10,10,10q�$��Center=�$ 0��$��42��.@��� $��All Archetypes���� -    ��Name$ FullNameTextBox��$��4��$��10,10,10,10q�$��Center=�$��28���� $��0��$��5�� �� Orientation$ 
Horizontal=�
������ $��
10,10,0,10q�$
��Left=�$	��200��.+��� $��Overall Range:��$��5q�
�������$��Center=��� -   $ OverallLowerBoundTextBox��$��30��$��0��$��30��$��5q��� $��to��$��5q�
�������$��Center=��� -   $ OverallHigherBoundTextBox��$��30��$��99��$��30��$��5q��� -	   $��6��$��Enter Player Stats��$��Center=�$��Center=�$��98���� -
   $��7��$��Calculate New Overall��$��Center=�$	��126��$��
0,10,0,402q��� -    ��Name$ PlayersList��$��1��$��0,10,0,0q�
������ $��0q�$��Disabled=�$
��Auto=�$
��1022��$��Right=�$
��1054��$	��Top=�$	��640�� �� MaxValue �� AlternationCount# Z $	��949��$��9������ .R��  � Orientation$ 
Horizontal=����� .R��� 
���   �$��1q�$��0q� �� Padding$ 0q�$	��150��$	��240��._��� .+��� 
������� �� TextAlignment$ Center=�$��14(�����  �� RelativeSource �� *FindAncestor+ �� AncestorType" �� �� Path$% (ItemsControl.AlternationIndex)� �� 	Converter# [  �� $	��150��$	��200��$��Center=����� *cardImage.url+�� 
�������$ Center=��� $�Rating: �� �� ��� *
newOverall+���� " ��.�-   