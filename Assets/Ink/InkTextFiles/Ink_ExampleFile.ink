//If player hasn't found the first inventory item in puzzle 1
Note: The subject is not exploring the initial area.
This is a reminder: failure to participate results in extermination


/*If the player puts in the wrong answer for puzzle 1
*/

//a divert that sends the dialogue to a section of text that is the knot main
//-> main 

//establishing a knot with the name main

/*
=== main ===
This is the content of the knot.



DO YOU LIKE ANIME
//using the brackets deletes that text from repeating in the answer
* YES! [I love it.] Thanks for asking!
     Me too!
     -> DONE
* [No]
    That's too bad. 


-> DONE

-> main2

=== main2 ===
What colors do you like best
*blue
    that's a basic color
    -> main2
*red
    That's my favorite
    -> main2
    //+ designates a sticky choice so it will always be available
+orange
    meh, i hate orange
    ->main2
* -> 
no more colors
-> DONE
// global variable. accessed anywhere in the story, can be numbers, text, or diverts
VAR Nameofvariable = 5
//variable that is contained to a specific section or knot
temp myTemporaryValue = 5

// similar to global variable but can't be changed aka is constant
CONST NameofConstant = 4
*/
/*
->main3
VAR pokemon = ""

=== main3 ===
Which pokemon do you want?
*pikachu
~ pokemon = "pikachu"
->chosePokemon
*Charmander
~ pokemon = "charmander"
->chosePokemon

    
    ===chosePokemon===
    You chose {pokemon}
->DONE
*/
//or you can pass in the variable by adding to the divert

/*
->main4
VAR pokemon1 = ""

=== main4 ===
Which pokemon do you want?
*pikachu
~ pokemon1 = "pikachu"
->chosePokemon1("pikachu")
*Charmander
~ pokemon1 = "charmander"
->chosePokemon1 ("charmander") 

    
    ===chosePokemon1(pokemon2)===
    You chose {pokemon1}
->DONE
*/
How's weather?
*sunny
    really
    ** yeah, really
    **no, not really
    //nested gather for when the nested bit reaches an end
    -- oh ok.
*snowy
*rainy
*foggy

//gather for when the entire conversation reaches an end
- that conversation was great


//you can also set your variable to false
VAR myVariable = true
{myVariable: This is written if yourVariable is true|Otherwise this is written}

VAR myvar = 4
{myvar < 5: This is written if var is less than 5 aka true | otherwise this is written}

VAR yourVariable = true
{yourVariable:
    This is written if yourVariable is true.
  - else:
    Otherwise this is written.
}

VAR myswitchcaseex = 3

{myswitchcaseex:
- 0: zero
- 1: one
- 2: two
- 3: three
-else: other number
}

VAR isTrue = true
VAR isFalse = false

*{isTrue} [choice 1]
*{isTrue} [choice 2]
*{isFalse} [choice 3]

//adding 
{5 +3}
{add (4,2)}
//using a function very similar to a knot
=== function add(a,b) ==
~ return a+b

//built in function Random will return a random result from the parameters, could be a number range or a LIST
//turns function will return how many turns you've done in the dialogue so failure

//you can tag something using a hashtag and the data will be stored with the dialogue line

//use tags to define a list, appearance, or add data to a line, 
//global tags to define if something is a bark and where it can occur


//outside of a knot call a stich with ->nameofknot.nameofstitch. from inside of the note it is called by ->stichname (using divert)

//you can include files by adding to the top of knot so you can reference knot, variables and other things from another file. Simple INCLUDE NameofFile (underlined) use the hamburger to add and check the box to make sure it's included

    
    










END
