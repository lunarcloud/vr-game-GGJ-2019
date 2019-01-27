...
 
-> Main

VAR PlayerName = "Bob"

VAR TalkingAboutResource = "Water"
VAR AmountFor = "Sell"
VAR NumpadValue = 0
VAR SuccessfulBuy = true
VAR SuccessfulSell = false

VAR HasTraded = false

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

 * Pra[y]ise the Lord.
 -> Praise
 * [Family]
 -> Home
 * [Joke]
 -> Joke
 * [Insult]
 -> Insult
 + [Back] -> Main

= Praise
{
    - Religion == "None": You say a quick prayer. They respectfully bow their head while you do so. 
    - Religion == "Not Discussed": You say a quick prayer, but it makes them uncomfortable to talk about such private things.  #friendliness:-0.1
    - Religion == "Praise Before Trading": 
    {
        - HasTraded == false :  You two say a quick prayer. #friendliness:0.1
        - HasTraded == true :  You go to pray, but they remind you not to do so before business. #friendliness:-0.1
    }
    - Religion == "Praise After Trading": 
    {
        - HasTraded == true :  You two say a quick prayer. #friendliness:0.1
        - HasTraded == false :  You go to pray, but they remind you not to do so before business. #friendliness:-0.1
    }
}
-> talk

= Home
How's your family? Are you all well?
{
    - Family == "None": 
        I don't like to discuss family with business partners. #friendliness:-0.1
        -> talk
    - Family == "Offer and Accept Visit": 
        -> HomeVisitOffer
    - Family == "Offer and Refuse Visit": 
        -> HomeVisitOffer
}

= HomeVisitOffer
You must visit the family.
 + [Accept]
    {
        - Family == "Offer and Accept Visit": Wonderful! They can't wait. It'll be great. #friendliness:0.1
        - Family == "Offer and Refuse Visit": They frown, you don't refuse a visit in this culture. #friendliness:-0.1
    }
 + [Refuse] TODO
    {
        - Family == "Offer and Accept Visit": They frown, you've broken the social norm of politely declining. #friendliness:-0.1
        - Family == "Offer and Refuse Visit": Well, maybe next time. They smile. #friendliness:0.1
    }
- -> talk

= Joke
You Tell A Joke.
{
    - Bantering == "Jokes Appreciated": They laugh heartily. #friendliness:0.1
    - Bantering == "Jokes Insulting": They frown at you. #friendliness:-0.1
    - else : Meh, heard better.
}
- -> talk

= Insult
You give them a good ribbing.
{
    - Bantering == "Insults Appreciated": They laugh heartily.  #friendliness:0.1
    - Bantering == "Insults Insulting": They frown at you. #friendliness:-0.1
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