_
 
-> Vendor.Conversation

VAR PlayerName = "Bob"
VAR CurrentPlayerOffer = 0
VAR CurrentVendorOffer = 0
VAR TalkingAboutResource = "Water"
VAR SuccessfulBuy = true
VAR SuccessfulSell = false

== Vendor ==

= Conversation

{Hello {PlayerName}, how are you?|} What are we doing?

* [buy] -> buy
* [sell] -> sell
* [talk] -> talk
* [goodbye] -> Goodbyes

= buy

What are you buying?

* [Water] #buy:Water:1  
    -> Conversation
* [Steel] #buy:Steel:1 
    -> Conversation
* [Uranium] #buy:Uranium:1 
    -> Conversation
* [Liquid Oxygen] #buy:LiquidOxygen:1 
    -> Conversation
* [Lithium] #buy:Lithium:1 
    -> Conversation
* [Nothing]
    -> Conversation

= sell

-> Conversation

= talk

-> Conversation

= Goodbyes

See you later!

-> DONE