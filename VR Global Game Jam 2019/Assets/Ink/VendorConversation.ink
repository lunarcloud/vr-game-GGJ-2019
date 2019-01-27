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
    ->->
+ [Steel]
    ~ TalkingAboutResource = "Steel"
    ->->
+ [Uranium]
    ~ TalkingAboutResource = "Uranium"
    ->->
+ [Liquid Oxygen]
    ~ TalkingAboutResource = "LiquidOxygen"
    ->->
+ [Lithium]
    ~ TalkingAboutResource = "Lithium"
    ->->
+ [Nothing]
    -> Main

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

-> talk

= Joke
TODO

-> talk

= Insult
TODO

-> talk

== Goodbyes ==
{
    - Friendliness == "Normal": Nice doing business with you.
    - Friendliness == "Low": Get outta here, swindler.
    - Friendliness == "High": Don't be a stranger. Say hi to your crew for me.
 } 

-> DONE