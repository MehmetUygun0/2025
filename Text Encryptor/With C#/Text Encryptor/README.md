# Encrypts text
### It encrypts the text and encrypts it differently each time. For example, a different result for each “hello” attempt
---
# How Does It Work
<img src="https://github.com/user-attachments/assets/5d6f89a2-8bda-4f52-8df9-13eeb7fa537c">

---
# How does it look?
![Ekran görüntüsü 2025-05-09 141050](https://github.com/user-attachments/assets/d616b006-9ba7-4f0a-9cd7-08f70cc05bb6)


---
# Details
Basic variables at hand:
Code1, code2, characters

`characters=[1,2,3 ... x,y,z](all characters)`

When the program first runs, it offers 2 options: text encryption(1) and text decryption(2)


Select (1)
- Prompts you to enter text 

`Example : “hello world”`

- Generates two 11-digit numbers 

`Example : code1=117565536788`

- Code1 is randomly generated according to the “Mode 10” algorithm

- After creating code1, we will create code2 according to code1. again 11 digits. like this

- We set our random seed according to code1

`example : random.seed(code1)`

- With random.Next(9) we take the mode of the 3rd power of each digit of code1

- So code2 becomes an 11-digit number again.

- We set our random seed according to code2 and randomly shuffle the sequence of characters

- We match our text in scrambled character order

and 'hello world' turned into ".s*-2 ‘s üa7?-’" (like this).

- We set our random seed to code1 and randomly mix ".s*-2 's üa7?-" result: "*.'-asü s2-7?"

- [x] As a result, we have a different encryption on each run.
Output from 3 different runs of "hello world"
  - > text : .M@;PfafMif code : 96351951672
  - > text : !fNN!NejW[q code : 532429104229
  - > text : eOfOIz<=z'z code : 6435810481068
