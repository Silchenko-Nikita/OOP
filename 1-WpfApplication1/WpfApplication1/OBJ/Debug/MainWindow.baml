   M S B A M L   `   `   `  ����  WpfApplication1     WpfApplication1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	    � 	Resources 5         
LabelStyle �� 
TargetType �� Property �� Value  GridViewHeaderStyle��%�   &	        &	 J     �� 5   
   " ��6#   .�5	      ��  � ��$
 Bold"�6+   5      �� 5   
   " ��5      .�5      ��  � ��$
 Bold"�6+   �� 5       � ��
 �  ��6+   �� 5       � ��
 ��·�6+   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��40 �6   �� 5      $��* �6   .+�5    
   o� $��0��6   .@�5!      m� $��_File��6   .@�5"      m� $��E_xit��5#       m� InputGestureText$ Ctrl-X��6&    m� Command 5"      �� *ExitCommand+5#   &   5$      5%      � 5'   
   $��1��6   $��Right=�6   $��0, 5, 20, 10q�5(      $��Center=�6!   .+�5)      ��  �� Orientation$ 
Horizontal=�6   .+�5*      �� #��[  6   Student name5+      �� 5,      $	��200��6   $��10, 0, 5, 0q�6&   ��5-      �� 	 �� Path$	 StudentNameToAdd�
 �� Mode$
 OneWayToSource=�.��5.   '   5/      �� 50      #��[  6   Score51      �� 52      $	��100��6   $��10, 0, 5, 0q�6&   ��53      �� $	 StudentScoreToAdd�$
 OneWayToSource=�.��54   +   55      �� 57      -   5         ��Name$ btnAddStudent��57      $��Add a student��58      ��59      �� *
AddStudent+5:      5;      5<      �� 5>   
   $��2��6   
�������$��Stretch=�5@      ��5?      �� $	 Students� �� View 5A      �� 5B      .P�5C      �� $
��Name��6%    �� HeaderContainerStyle# [ 5D   %    �� DisplayMemberBinding 5F   %   �� $
	 Name��� 5G      $��Score��6%   # [ 5H   %    5J   %   �� $	 Score��� 5K      $��	TimeAdded��6%   # [ 5L   %    5N   %   �� $	 	TimeAdded��� 5O      $��Comment��6%   # [ 5P   %    5R   %   �� $	 Comment�5S      5U      5V      5W      