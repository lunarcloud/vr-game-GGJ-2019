INCLUDE Jokes.ink
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
VAR Family = "Offer and Accept Visit" //, None, Offer and Refuse Visit, Not Discussed
VAR Bantering = "Jokes Appreciated" //, None, Jokes Insulting, Insults Appreciated, Insults Insulting

== Main ==
"<>{
    - Friendliness == "Low":{Hello {PlayerName}...|} What do you want?
    - Friendliness == "High":{Hello {PlayerName}, it's good to see you.|} What would you like to talk about?
    - else:{Hello {PlayerName}, welcome.|} How can I help you?
}<>" they say.

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

"How Much?" #numpadShow
[buying...] #buy
{SuccessfulBuy : Awesome | Sorry, not enough money for that. } #numpadHide
-> Main

== sell ==
What are you selling?
~ AmountFor = "Sell"

-> SelectResource -> SelectAmountSell

= SelectAmountSell
How Much? #numpadShow
[selling...] #sell
{SuccessfulBuy : Awesome | Sorry, you don't have that many. } #numpadHide
-> Main

== talk ==
What do you talk about?
 * [Pray]
 -> Praise
 * [Family]
 -> Home
 * [Joke]
 -> Joke
 * [Insult]
 -> Insult
 + [Back]
 -> Main

= Praise
{
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
    - else: You say a quick prayer. They respectfully bow their head while you do so. 
}
-> talk

= Home
"How's your family? Are you all well?" you ask.
{
    - Family == "Not Discussed": 
        "Are you threatening me?" they say. You shift uncomfortably and change the subject. #friendliness:-0.1
        -> talk
    - Family == "Offer and Accept Visit": 
        -> HomeVisitOffer
    - Family == "Offer and Refuse Visit": 
        -> HomeVisitOffer
    - else: "They're... uh... fine, I guess," they say.
        -> talk
}

= HomeVisitOffer
"You must visit the family," they say.
 + [Accept]
    {
        - Family == "Offer and Accept Visit": "Wonderful! They can't wait. It'll be great," they say. #friendliness:0.1
        - Family == "Offer and Refuse Visit": They frown, you don't refuse a visit in this culture. #friendliness:-0.1
    }
 + [Refuse]
    {
        - Family == "Offer and Accept Visit": They frown, you've broken the social norm of politely declining. #friendliness:-0.1
        - Family == "Offer and Refuse Visit": They smile. "Well, maybe next time"  #friendliness:0.1
    }
- -> talk

= Joke
You tell a joke:
"<>{~-> Joke1 -> JokeEnd|-> Joke2 -> JokeEnd|-> Joke3 -> JokeEnd|-> Joke4 -> JokeEnd|-> Joke5 -> JokeEnd|-> Joke6 -> JokeEnd}

= JokeEnd
<>" 
{
    - Bantering == "Jokes Appreciated": They laugh heartily. #friendliness:0.1
    - Bantering == "Jokes Insulting": They frown at you. #friendliness:-0.1
    - Bantering == "Insults Appreciated": "Not my kind of humor," they say.
    - else: They smile and nod a little. You change the subject.
}
-> talk

= Insult
You give them a good ribbing.
{
    - Bantering == "Insults Appreciated": They laugh heartily.  #friendliness:0.1
    - Bantering == "Insults Insulting": They frown at you. #friendliness:-0.1
    - Bantering == "Jokes Appreciated": "Not my kind of humor," they say.
    - else: They smile a little and raise an eyebrow. You change the subject.
}
-> talk

== Goodbyes ==
"<>{
    - Friendliness == "Low":Get outta here, swindler.
    - Friendliness == "High":Don't be a stranger. Say hi to your crew for me.
    - else:Nice doing business with you.
}<>"
-> DONE