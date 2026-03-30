##Defines different folders and their purposes:

###Prompt

* A task instruction
* Usually one-shot
* Example: "Review this code against architecture.md and patterns.md"

###Skill

* A reusable capability
* Usually narrower than an agent
* Example: "Generate a thin controller using existing service patterns"

###Agent

* A coordinator
* Uses prompts and skills
* Handles workflow
* Example: "For a PR, load docs, run architecture review, summarize violations"

###Verifier

* A policy/checking prompt or review standard
* Defines pass/fail criteria
* Should be the source of truth for architecture review rules

###Good Test

If I delete the agent file, do I still know the review rules?

* Yes = good separation

If I delete the verifier file, can the agent still review correctly?

* No = good separation

If both files each contain the full rules:

* bad design

