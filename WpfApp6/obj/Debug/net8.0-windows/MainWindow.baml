   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   B >WpfApp6, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:JsonDataDisplayJsonDataDisplay 5   	       JsonDataDisplay.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   /localclr-namespace:JsonDataDisplay     5   	     � Title$  XXV-Rate��5   	   $
��1000��6   $
��1900��6(    � WindowState$ 	Maximized=�65    � 	Resources 5       �� 
TargetType �� Property �� Value(  "JsonDataDisplay.IncrementConverter   IncrementConverter��%  '��       '� x     &	  �     �� 5	   
   " ��6   .�5
      ��  � �� �#3333336+   �� 5       � �� �#CCCCCC6+   5      �� 5   
   " �6   .�5      ��  � �� �#1E1E1E6+   �� 5       � ��
 �����6+   5        5   
   6#   5      .��5      � $��Stretch=�6    � ColumnDefinitions 5   
   �� 5      $��1437* �6   �� 5      $
��233* �6   �� 5      $
��230* �6   .+�5   
   r� -   5         r�Name$ BackgroundVideo��5       r� Source$ background.mp4?�60   	 r� LoadedBehavior$
	 Play=�6H   
 r� IsMuted$

 True��6^    r� Stretch$ UniformToFill=�6m   $��3��6�   � 5    
   $��3��6    5"      �� 5#      $
��Auto �6#   �� 5$      $��* �6#    � RowDefinitions 5(      �� 5)      $
��Auto �6    �� 5*      $
��Auto �6    �� 5+      $
��Auto �6    �� 5,      $
��Auto �6    �� 5-      $
��Auto �6    �� 5.      $
��Auto �6    �� 5/      $
��Auto �6    �� 50      $
��Auto �6    �� 51      $��* �6    .+�55      �� -   5         ��Name$ TeamFilterComboBox��55      $��0��63   $��Center=�6C    �� SelectedIndex$ 0��6^   $��42��6�   $��10,10,10,10q�6�   .@�56      �� $��	All Teams��6   57      �� 5:      -   5        $ PositionFilterComboBox��5:      $��0��67   $��1��6G   $��Center=�6T   $ 0��6o   $��42��6�   $��10,10,10,10q�6�   .@�5;      �� $��All Positions��6   5<      �� 5?      -   5        $ ProgramFilterComboBox��5?      $��0��66   $��2��6F   $��10,10,10,10q�6S   $��Center=�6h   $ 0��6�   $��42��6�   .@�5@      �� $��All Programs��6   5A      �� 5D      -   5        $ ArchetypeFilterComboBox��5D      $��0��68   $��3��6H   $��10,10,10,10q�6U   $��Center=�6j   $ 0��6�   $��42��6�   .@�5E      �� $��All Archetypes��6   5F      �� 5I      -   5         ��Name$ FullNameTextBox��5I      $��4��6/   $��10,10,10,10q�6<   $��Center=�6Q   $��28��6�   �� 5M      $��0��6   $��5��6)    �� Orientation$ 
Horizontal=�66   
������ $��
10,10,0,10q�6h   $
��Left=�6|   $	��200��6�   .+�5N      �� $��Overall Range:��6   $��5q�62   
�������$��Center=�6P   �� 5O      -   5        $ OverallLowerBoundTextBox��5O      $��30��6<   $��0��6G   $��30��6P   $��5q�6\   �� 5P      $��to��6   $��5q�6&   
�������$��Center=�6D   �� 5Q      -   5        $ OverallHigherBoundTextBox��5Q      $��30��6=   $��99��6H   $��30��6R   $��5q�6^   5R      �� 5U      -	   5        $��6��5U      $��Enter Player Stats��6"   $��Center=�6^   $��Center=�6{   $��98��6�   �� 5Y      -
   5        $��7��5Y      $��Calculate New Overall��6"   $��Center=�6d   $	��126��6�   $��
0,10,0,402q�6�   �� 5_      -   5         ��Name$ PlayersList��5_      $��1��6+   $��0,10,0,0q�6;   
������ $��0q�6f   $��Disabled=�5`      $
��Auto=�5a      $
��1022��5b      $��Right=�6&   $
��1054��6B   $	��Top=�5c      $	��640��5d       �� MaxValue �� AlternationCount# Z 5f      $	��949��6y   $��9��6�   ��5g      �� 5h      .R�5i      �  � Orientation$ 
Horizontal=�6$   5j      ��5l      �� 5m      .R�5n      �� 
���   �$��1q�65   $��0q�6I    �� Padding$ 0q�6T   $	��150��6`   $	��240��6l   ._�5o      �� .+�5q   "   �� 
������� �� TextAlignment$ Center=�5s   ,   $��14(�5t   ,   ��5q   ,   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" �� �� Path$% (ItemsControl.AlternationIndex)� �� 	Converter# [  5t   ,   �� 5v   "   $	��150��6I   $	��200��6U   $��Center=�6b   ��6(   �� *cardImage.url+6b   �� 5x   "   
�������$ Center=�6?   �� 5y   &   $�Rating: ��6*    �� 5z   &   �6*   �� *
newOverall+5{   #   5}      5      5�      ��5�      �� 5�      " ��6   .�5�      -   5        5�      5�      5�      5�      5�      