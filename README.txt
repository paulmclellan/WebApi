Assumptions:
1. Student Names are unique.
2. There is a maximum of one Student per Mark/Time combination.

Improvements:
1. Support multiple Students per Mark/Time combination.

Additional Points to note:
1. I added a basic logger interface to demonstrate Dependency Injection.

Instructions to call the Web Api:

Postman
-------
POST 
http://localhost:58855/api/studentgroups

Headers
Content-Type	application/json

Bodyraw (application/json)
[
["","","Simon","",""],
["","Sergey","","Thomas",""],
["","","","",""],
["","Chris","","",""],
["","Harry","","Roger",""],
["","","","",""]
]



