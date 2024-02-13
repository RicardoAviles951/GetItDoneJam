VAR answers = 0
VAR turns = 0


Welcome Volunteer 732, Citizen Classification Blue X

The sociological assessment will determine your understanding of THE SOCIETY and your acceptance of it. Elevating your position in THE SOCIETY cannot be done lightly. When you are ready, proceed.
#CLEAR

*[BEGIN EXAMINATION]
-->Questions

===Questions===
Evaluate the following statement:
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

Some roles in a society have more value than other roles.
->answerchoices

===Question3 ===

Those in a society knows what's best for the society.
->answerchoices

===Question4 ===

Progress is important to a continually developing any society.
->answerchoices

===Question5 ===

Peace is more important than truth.
->answerchoices

===Question6 ===

The value of the next generation is a measure of worth in a society.
->answerchoices

===Question7 ===

Trust given should be reciprocated.
->answerchoices

===Question8 ===

Everything in a society has a value.
->answerchoices

===Question9 ===

Individuals in a society can become more or less valuable to that society.
->answerchoices

===Question10 ===

The citizen color ranking adequately measures an individual's worth in a society.
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
Thank you Volunteer 732. Your results will be calculated, and you will be rewarded based on your efforts. 

#ActivateFire

When you exit the examination, you will discover your prosthetic has been upgraded. A thanks for your participation.

Follow the hand. It will guide you.

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
Volunteer 732,

Completion of the entire test will result in a citizen rank elevation. As such, you will continued to be tested.

The results of this questionnaire have warranted a general health decrease. May you continue to understand the dynamics of THE SOCIETY.

#outcome1
*[ACCEPT RESULTS]
-->FINAL

===RESULT2===
Volunteer 732,

Completion of the entire test will result in a citizen classification elevation. As such, you will continued to be tested.

The results of this questionnare have warranted a general movement decrease. May you continue to understand the dynamics of THE SOCIETY.
#outcome2
*[ACCEPT RESULTS]
-->FINAL
===RESULT3===
Volunteer 732,

Completion of the entire test will result in a citizen classification elevation. As such, you will continued to be tested.

The results of this questionnare have warranted a general movement increase. It appears you have some understanding of the dynamics of THE SOCIETY.
#outcome3
*[ACCEPT RESULTS]
-->FINAL
===RESULT4===
Volunteer 732,

Completion of the entire test will result in a citizen classification elevation. As such, you will continued to be tested.

The results of this questionnare have warranted a general health increase. It appears you have some understanding of the dynamics of THE SOCIETY.

#outcome4
*[ACCEPT RESULTS]
-->FINAL

===FINAL===
#CLEAR

This concludes the sociological assessment portion of this test. Thank you for volunteering.

Eden awaits.
*[EXIT]
-->END






