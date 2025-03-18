import asyncio
import random
import string

main_characters = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '!', '"', '#', '$', '%', '&', "'", '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~', ' ']
characters = main_characters.copy()
code=""
code2=""
ciphertext=""

def Code2_Creat():
    global code2
    new_number=0
    number_power=0
    random.seed(int(code))  
    for i in range(11):
            number_power = int(code[i]) ** 3
            new_number = number_power % random.randint(1, 9)  
            code2 += str(new_number)
    number = str(code2)
    code2 = int(number.lstrip('0'))

def Code_Create():
    global code
    global code2
    for i in range(11):
        code+=str(random.randint(1,10))

    singles = sum(int(code[i]) for i in range(0, 9, 2))
    doubles = sum(int(code[i]) for i in range(1, 8, 2))
    total = sum(int(code[i]) for i in range(10))

    if (singles * 7 - doubles) % 10 == int(code[9]) and total % 10 == int(code[10]):
        Code2_Creat()
    else:
        code=""
        Code_Create()
    
def Mix(metin):
    swaps=[]
    global ciphertext
    Code_Create()
    charactersi_Mix()

    indisler = [main_characters.index(c) for c in metin]
    for i in indisler:
        ciphertext+=characters[i]
    ciphertext_list=list(ciphertext)

    random.seed(int(code))
    for i in range(len(ciphertext)):
        index = random.randint(0, len(ciphertext) - 1)
        swaps.append((i, index))
        ciphertext_list[i], ciphertext_list[index] = ciphertext_list[index], ciphertext_list[i]
    ciphertext="".join(ciphertext_list)
    
    print("Ciphertext : ",ciphertext)
    print("Your code : ",code)

def charactersi_Mix():
    random.seed(int(code2))
    for i in range(len(characters)):
        index = random.randint(0, len(characters) - 1)
        characters[i], characters[index] = characters[index], characters[i]

def Arrange(text,code):
    global ciphertext
    swaps=[]
    ciphertext=text
    Code2_Creat()
    charactersi_Mix()

    random.seed(int(code))
    for i in range(len(ciphertext)):
        index = random.randint(0, len(ciphertext) - 1)
        swaps.append((i, index))
    
    ciphertext_list=list(ciphertext)
    for i, index in reversed(swaps):
        ciphertext_list[i], ciphertext_list[index] = ciphertext_list[index], ciphertext_list[i]
    ciphertext="".join(ciphertext_list)

    indisler = [characters.index(c) for c in ciphertext]
    corrected_text=""
    for i in indisler:
        corrected_text+=main_characters[i]
    print("Deciphered text : ",corrected_text)
    
def main():
    global code
    global code2

    print("The text you enter is encrypted. And you are given a code. This code is required to decrypt your encrypted text.")
    print("To encrypt text 1")
    print("To parse the text 2")
    secim=input("Select(1/2) : ")
    if secim=="1":
        text =input("Enter text : ")
        Mix(text)
    elif secim=="2":
        text=input("Enter the encrypted text : ")
        code=input("Enter your code : ")
        Arrange(text,code)
    else:
        print("Please choose\n")
        main()
    input()
main() 