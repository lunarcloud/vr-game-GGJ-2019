...
 
-> Vendor.Conversation

VAR PlayerName = "Bob"
VAR NumpadValue = 0
VAR TalkingAboutResource = "Water"
VAR AmountFor = "Sell"
VAR SuccessfulBuy = true
VAR SuccessfulSell = false
LIST Friendliness = Low, Normal, High
~ Friendliness = Normal

== Vendor ==

= Conversation

{Hello {PlayerName}, how are you?|} What are we doing?

+ [buy] -> buy
+ [sell] -> sell
+ [talk] -> talk
+ [goodbye] -> Goodbyes

= buy

What are you buying?
~ AmountFor = "Buy"

+ [Water]
    ~ TalkingAboutResource = "Water"
    -> SelectAmountBuy
+ [Steel]
    ~ TalkingAboutResource = "Steel"
    -> SelectAmountBuy
+ [Uranium]
    ~ TalkingAboutResource = "Uranium"
    -> SelectAmountBuy
+ [Liquid Oxygen]
    ~ TalkingAboutResource = "LiquidOxygen"
    -> SelectAmountBuy
+ [Lithium]
    ~ TalkingAboutResource = "Lithium"
    -> SelectAmountBuy
+ [Nothing]
    -> Conversation

= sell

-> Conversation

= SelectAmountBuy

. #numpadShow #background:office
{SuccessfulBuy : Awesome | Sorry, not enough money for that. }

-> Conversation

= talk

-> Conversation

= Goodbyes

See you later!

-> DONE