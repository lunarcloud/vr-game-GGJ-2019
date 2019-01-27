...
 
-> Main

VAR PlayerName = "Bob"

VAR TalkingAboutResource = "Water"
VAR AmountFor = "Sell"
VAR NumpadValue = 0
VAR SuccessfulBuy = true
VAR SuccessfulSell = false

VAR Friendliness = "Normal" //, Low, High
VAR Religion = "Not Discussed" //, None, Praise Before Trading, Praise After Trading
VAR Family = "Offer and Accept Visit" //, None, Offer and Accept Visit, Offer and Refuse Visit
VAR Bantering = "Jokes Appreciated" //, None, Jokes Insulting, Insults Appreciated, Insults Insulting

== Main ==
{
    - Friendliness == "Normal": {Hello {PlayerName}, welcome.|} How can I help you?
    - Friendliness == "Low": {Hello {PlayerName}...|} What do you want?
    - Friendliness == "High": {Hello {PlayerName}, it's good to see you.|} What would you like to talk about?
 } 

+ [buy] -> buy
+ [sell] -> sell
+ [talk] -> talk
+ [goodbye] -> Goodbyes

== SelectResource ==

+ [Water]
    ~ TalkingAboutResource = "Water"
+ [Steel]
    ~ TalkingAboutResource = "Steel"
+ [Uranium]
    ~ TalkingAboutResource = "Uranium"
+ [Lox]
    ~ TalkingAboutResource = "Lox"
+ [Lithium]
    ~ TalkingAboutResource = "Lithium"
+ [Nothing]
    -> Main
    
- ->->
== buy ==

What are you buying?
~ AmountFor = "Buy"

-> SelectResource -> SelectAmountBuy

= SelectAmountBuy

How Much? #numpadShow
[buying...] #buy

{SuccessfulBuy : Awesome | Sorry, not enough money for that. }

-> Main

== sell ==

What are you selling?
~ AmountFor = "Sell"

-> SelectResource -> SelectAmountSell

= SelectAmountSell

How Much? #numpadShow
[selling...] #sell

{SuccessfulBuy : Awesome | Sorry, you don't have that many. }


-> Main

== talk ==

[What do you talk about?]

 + Pra[y]ise the Lord.
 -> Praise
 + [Family]
 -> Home
 + [Joke]
 -> Joke
 + [Insult]
 -> Insult
 + [Back] -> Main

= Praise
TODO

-> talk

= Home
How's your family? Are you all well?
{
    - Family == "None": TODO 
        bad #friendliness:-0.1
    -> talk
    - Family == "Offer and Accept Visit": TODO 
    -> HomeVisitOffer
    - Family == "Offer and Refuse Visit": TODO 
    -> HomeVisitOffer
}

= HomeVisitOffer
You must visit the family.
 + [Accept]
    {
        - Family == "Offer and Accept Visit": TODO 
            good #friendliness:0.1
        - Family == "Offer and Refuse Visit": TODO 
            bad #friendliness:-0.1
    }
 + [Refuse] TODO
    {
        - Family == "Offer and Accept Visit": TODO
            bad #friendliness:-0.1
        - Family == "Offer and Refuse Visit": TODO 
            good #friendliness:0.1
    }
- -> talk

= Joke
You Tell A Joke.
{
    - Bantering == "Jokes Appreciated": TODO 
        good #friendliness:0.1
    - Bantering == "Jokes Insulting": TODO 
        bad #friendliness:-0.1
    - else : Meh, heard better.
}
- -> talk

= Insult
You Insult their mother.
{
    - Bantering == "Insults Appreciated": TODO 
        good #friendliness:0.1
    - Bantering == "Insults Insulting": TODO 
        bad #friendliness:-0.1
    - else : Meh, heard better.
}
- -> talk

== Goodbyes ==
{
    - Friendliness == "Normal": Nice doing business with you.
    - Friendliness == "Low": Get outta here, swindler.
    - Friendliness == "High": Don't be a stranger. Say hi to your crew for me.
 }
-> DONE