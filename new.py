import os
import matplotlib.pyplot as plt
import numpy as np
from matplotlib import colors
from matplotlib.ticker import PercentFormatter
import math

cmFille=open("in//pre_train4_conf_ev.txt","r+")
classFille=open("in//pre_train4_classes.txt","r+")




table=cmFille.readlines()
tables=[]

string=""
for x in table:
    string+=x.replace("\n","")
    if "] [" in string:
        string2=string.split("] [")
        tables.append(string2[0].replace("]","").replace("[", "").replace("  "," ").replace("  "," ").replace(" ",",").replace("0",""))
        string=string2[1]
        
tables.append(string.replace("]","").replace("[", "").replace(" ",",").replace("0",""))

for x in range(len(tables)):
    if tables[x][0]==',':
        tables[x]=tables[x][1:]
        
    if tables[x][len(tables[x])-1]==',':
        tables[x]=tables[x][:-1]

n=0
for x in tables[0]:
   if x==',':
       n+=1

       
allacc=[]

cl=classFille.readlines()
for x in range(len(tables)):
    smalltab=tables[x].split(",")
    bad=0
    for t in range(len(smalltab)):
        if smalltab[t] !='':
            if t==x:
                good=float(smalltab[t])
            else:
                bad+=float(smalltab[t])
   
    allacc.append([good/(bad+good),cl[x].replace("\n","")])    
    
    
cmFille.close()
classFille.close()


allacc.sort(key=lambda x:x[0])

dane=[allacc[0],allacc[-1]]#min,max
suma=0
for x in allacc:
    suma+=x[0]


oX=suma/len(allacc)
dane.append(suma/len(allacc))#srednia
dane.append(allacc[int(len(allacc)/2)][0])#mediana

odchylenie=0
for a in allacc:
    odchylenie+=math.pow((a[0]-oX),2)
dane.append(math.sqrt(odchylenie/len(allacc)))#odchylenie
    
print(dane)

ki=[]
kn=[]
for alc in allacc:
    if alc[0]<0.8:
        ki.append(alc)
    else:
        kn.append(alc)





f=open("under80.txt","w+")

for n in ki:
    f.write("[ "+str(n[0])+", "+n[1]+" ]\n")
f.close()

fi=open("over80.txt","w+")
for n in kn:
    fi.write("[ "+str(n[0])+", "+n[1]+" ]\n")
fi.close()



"""
newb=[]
newa=[]

r=0
for i in range(100):
    newa.append(i+1)
    k=0
    for x in allacc:
        if i/100<x[0] and x[0]<=(i/100)+0.01:
            k+=1
    newb.append(k)
        


plt.bar(newa,newb)
plt.ylabel('number of class')
plt.xlabel('achieved accuracy')

plt.show()

print(allacc[0])

""
"""

