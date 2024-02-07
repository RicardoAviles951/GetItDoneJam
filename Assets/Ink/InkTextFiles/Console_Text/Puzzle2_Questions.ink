VAR answers = 0
VAR turns = 0


Welcome Volunter 47182, Citizen Classification Blue X
The questions on this examination will determine your understanding of our society and your acceptance of it. Changing the dynamics of your position in this society cannot be done lightly. When you are ready, proceed.
#CLEAR

*[BEGIN EXAMINATION]
-->Questions

===Questions===
How do you feel about the following statement:
{turns==0: ->Question1|->QuestionBranchManager}

->DONE


===QuestionBranchManager===

{turns:
-1:
{answers < 0: ->Question2 | ->Question3}
-2:
{answers < 0: ->Question4 | ->Question5}
-3:
{answers < 0: ->Question6 | ->Question7}
-4:
{answers < 0: ->Question8 | ->Question9}
-else: 
{answers < 0: ->Question10 | ->Question10}
}


->DONE
===Question1 ===

Every person has a role to play in a developed society.
->answerchoices

===Question2 ===

Some roles in a society are more important than other roles.
->answerchoices

===Question3 ===
The Society knows what's best for the society.

->answerchoices

===Question4 ===
question 4

->answerchoices

===Question5 ===

question 5
->answerchoices

===Question6 ===
question 6
->answerchoices

===Question7 ===
question 7
->answerchoices

===Question8 ===
question 8
->answerchoices

===Question9 ===
question 9
->answerchoices

===Question10 ===
Do you consider your current position BLUE X of high value to the society?

->answerchoices


//answer choices to each of the questions
===answerchoices ===


+[strongly disagree]
 ~answers = answers - 2
  ~turns = turns +1 
+[disagree]
~answers = answers - 1
  ~turns = turns +1  
+[neutral]
~answers = answers + 0
  ~turns = turns +1  
+[agree]
~answers = answers +1
  ~turns = turns +1  
+[strongly agree]
~answers = answers +2
  ~turns = turns +1  
  
#CLEAR
-{turns == 6: ->WRAPUP| ->Questions}

===WRAPUP===
Thank you Volunteer 47182. Your results will be calculated, and you will be rewarded based on your efforts. 

When you exit the examination, you will discover your prosthetic has been upgraded again. A thanks for your participation.

#CLEAR

*[CONTINUE]
-->RESULTS

->DONE

===RESULTS===
 { 
 -answers < -6: ->RESULT1
 -answers <0: ->RESULT2
 -answers <6: ->RESULT3
 -else: ->RESULT4
 }
 
 
===RESULT1===
result 1
#outcome1
->FINAL

===RESULT2===
result 2
#outcome2
->FINAL
===RESULT3===
result 3
#outcome3

->FINAL
===RESULT4===
result 4
#outcome4
->FINAL

===FINAL===
#CLEAR

end of examination
->END






