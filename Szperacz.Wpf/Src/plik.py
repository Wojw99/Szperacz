# coding=ascii

import os, fitz
from docx import Document
import matplotlib.pyplot as plt
import concurrent.futures
import regex




class TargetFille:
    def __init__(self,FilesTab,Target,IgnoreSize,Ext):
        self.FilesTab=FilesTab
        self.Target=Target
        self.IgnoreSize=IgnoreSize
        self.Ext=Ext
        self.n_Target=len(Target)

        
        
        
    def GiveAllFilles(path):
        #txt/docx/pdf
        files = [[],[],[]]
        size=0
        # r=root, d=directories, f = files
        for r, d, f in os.walk(path):
            for file in f:
                if '.txt' in file:
                    files[0].append(os.path.join(r, file))
                    size+=1
                if '.doc' in file:
                    files[1].append(os.path.join(r, file))
                    size+=1
                if '.pdf' in file:
                    files[2].append(os.path.join(r, file))
                    size+=1

        return size,files

    def NoSpace(tab):
        
        newTab=[]
        for x in tab:
            for y in x.split(" "):
                newTab.append(y)
        return newTab

    def NoEnter(tab):#usowa z endlin() z configa
        for n in range(len(tab)):
            tab[n]=tab[n].replace("\n","")
        return tab
    
    def ConfigurationFile():

        with open("./config.txt","r", encoding="utf-8") as config:
            txtConfig=TargetFille.NoEnter(config.readlines())
            Target=txtConfig[0].split(" ")
            Size, Files=TargetFille.GiveAllFilles(txtConfig[1])
            
            if "0" in txtConfig[2]:#czy wielkosc liter ma znaczenie
                IgnoreSize=False
            else:
                IgnoreSize=True
            try: 
                ThreadNumber= int(txtConfig[3]) # ile wątkow ma zostac uruchomina
            except:
                ThreadNumber=1

                 
        return Target, Size, Files, IgnoreSize, ThreadNumber
    
    
    def FindText(self):
        FilesTab=self.FilesTab
        Target=self.Target
        IgnoreSize=self.IgnoreSize
        n_Target=self.n_Target
        n_pointer=0
        endTable=[]
        
        if FilesTab!=[]:
            for file in FilesTab:
                try:
                    with open(file,"r",encoding="utf-8") as txtFile:
                        endTable.append([file,0])
                        allLines=TargetFille.NoEnter(TargetFille.NoSpace(txtFile.readlines()))
                        for line in allLines:
                            if regex.MATCH(line.upper(),Target[n_pointer].upper()):
                                if regex.MATCH(line,Target[n_pointer]) or IgnoreSize:
                                    n_pointer+=1
                                    if n_pointer==n_Target:
                                        endTable[-1][1]+=1
                                        n_pointer=0
                                else:
                                    n_pointer=0 
                            else:
                                n_pointer=0
                               
                        if endTable[-1][1]==0:
                            endTable.pop()
                except Exception as e:
                    print("Error in txtReader",e)
        return endTable
    
    def FindDocx(self):
        
        FilesTab=self.FilesTab
        Target=self.Target
        IgnoreSize=self.IgnoreSize
        n_Target=self.n_Target
        n_pointer=0
        endTable=[]
        
        if FilesTab!=[]:
            for files in FilesTab:
                try:
                    doc = Document(files)
                    endTable.append([files,0])
                    for para in doc.paragraphs:          
                            if regex.MATCH(para.text.upper(),Target[n_pointer].upper()):
                                if regex.MATCH(para.text,Target[n_pointer]) or IgnoreSize:
                                    n_pointer+=1
                                    if n_pointer==n_Target:
                                        endTable[-1][1]+=1
                                        n_pointer=0
                                else:
                                    n_pointer=0 
                            else:
                                n_pointer=0
                                
                    if endTable[-1][1]==0:
                        endTable.pop()
                except Exception as e:
                        print("Error in docxReader:",e)
        return endTable
    
    
    def FindPdf(self):

        FilesTab=self.FilesTab
        Target=self.Target
        IgnoreSize=self.IgnoreSize
        n_Target=self.n_Target
        n_pointer=0
        endTable=[]
        
        if FilesTab!=[]:
            for files in FilesTab:
                try:
                    endTable.append([files,0])
                    pdf = fitz.open(files)
                    pdfSize=pdf.pageCount
                    for i in range(pdfSize):
                        page = pdf.loadPage(i)
                        text=page.getText("text").split(" ")
                        for line in text:
                            if regex.MATCH(line.upper(),Target[n_pointer].upper()):
                                if regex.MATCH(line,Target[n_pointer]) or IgnoreSize:
                                    n_pointer+=1
                                    if n_pointer==n_Target:
                                        endTable[-1][1]+=1
                                        n_pointer=0
                                else:
                                    n_pointer=0 
                            else:
                                n_pointer=0
                                
                                    
                    if endTable[-1][1]==0:
                        endTable.pop()
                except Exception as e:
                        print("Error in pdfReader:",e)
        return endTable


    def SumAllReads(files):
        
        ListOfFiles=[]
        if files[0].FilesTab!=[]:
            ListOfFiles.append(files[0].FindText())
        if files[1].FilesTab!=[]:
            ListOfFiles.append(files[1].FindDocx())
        if files[2].FilesTab!=[]:
            ListOfFiles.append(files[2].FindPdf())        

        suma=0
        maximum=0
        minimum=0
        start=True
        for m in ListOfFiles:
            for n in m:
                suma+=n[1]
                if n[1]>maximum or start:
                    maximum=n[1]
                if n[1]<minimum or start:
                    minimum=n[1]
                if start:
                    start=False
        
     
        return ListOfFiles, suma, maximum, minimum


    def returnList(tab):
        with open("connector.txt","w+") as connector:
            for column in tab:
                for line in column:
                    connector.write(line[0]+"; "+str(line[1])+"\n")
    
    def pieChart(allnr,tab):
        label=[]
        amount=[]
        sal=0
        for column in tab:
                for line in column:
                    sal+=1
                    if line[1] in label:
                        amount[label.index(line[1])]+=1
                    else:
                        label.append(line[1])
                        amount.append(1)
        allnr-=sal
        if allnr>0:
            label.append(0)
            amount.append(allnr)
            #print(allnr)
                        
                        
                        
        fig1, ax1 = plt.subplots()
        ax1.pie(amount, labels=label, autopct='%1.1f%%',startangle=90)
        ax1.axis('equal')
        
        plt.show()#zminić na zapis
        
        
    def barChart(alltab,tab):

        amount=[0,0,0]
        allamount=[0,0,0]
        
        for c in range(len(tab)):
                for line in tab[c]:
                    amount[c]+=1
       # print(amount)
        for c in range(len(alltab)):
                for line in alltab[c]:
                    allamount[c]+=1
                allamount[c]-=amount[c]
       # print(allamount)             
        
    
        labels = ['.txt', '.docx', '.pdf']
    
    
        width = 0.35       # the width of the bars: can also be len(x) sequence
        
        fig, ax = plt.subplots()
        
        ax.bar(labels, amount, width, label='Detected')
        ax.bar(labels, allamount, width, bottom=amount,color="gray",label='All')
        
        ax.set_ylabel('Number of files')
        ax.set_title('Distribution by extensions')
        ax.legend()
        
        plt.show()
        
        
        
    def deleteEmpty(tab):
        for x in range(len(tab)):
            if tab[x]==[]:
                tab.pop(x)
                TargetFille.deleteEmpty(tab)
                break
        return tab
        
    def dtf(tab):
        ntab=[]
        if len(tab)==2 and type(tab[1])==int:
            return tab
        for x in tab:
            ntab.extend(TargetFille.dtf(x))
        return ntab
    
    def nextdtf(tab):
        tab=TargetFille.dtf(tab)
        newtab=[[],[],[]]
        for x in range(int(len(tab)/2)):
            if "txt" in tab[x*2]:        
                newtab[0].append([tab[x*2],tab[x*2+1]])
            
            if "doc" in tab[x*2] or "docx" in tab[x*2]:        
                newtab[1].append([tab[x*2],tab[x*2+1]])
                              
            if "pdf" in tab[x*2]:        
                newtab[2].append([tab[x*2],tab[x*2+1]])
        return(newtab)   
        
            
            



#zamienic jako arg
def main():

    Target, Size, Files, IgnoreSize, ThreadNumber=TargetFille.ConfigurationFile()
    
    
    threads=[]
    
    ltab=[]
    sums=0
    for x in range(3):
        sums+=len(Files[x])
    if sums<ThreadNumber:
        ThreadNumber=sums
        
    
    for i in range(ThreadNumber):
        threads.append([[],[],[]])
        
        
    for x in range(3):
        ltab.append(len(Files[x]))
    k=0
    for x in range(max(ltab)):
        for y in range(3):
            if ltab[y]>x:
                threads[k%ThreadNumber][y].append(Files[y][x])
        
        
        k+=1     
    
    NumberOfFilles=0
    for x in range(3):
        NumberOfFilles+=len(Files[x])
        
    ListOfFiles=[]
    suma=0
    maximum=0
    minimum=0
    nee=True
    
    
    with concurrent.futures.ThreadPoolExecutor() as executor:
        futures = []
        for thread in threads:
            ObjectFiles=[] 
            ObjectFiles.append(TargetFille(thread[0], Target, IgnoreSize,'.txt'))     
            ObjectFiles.append(TargetFille(thread[1], Target, IgnoreSize,'.doc'))   
            ObjectFiles.append(TargetFille(thread[2], Target, IgnoreSize,'.pdf'))
            #newlist, newSuma, newmaximum, newminimum = TargetFille.SumAllReads(ObjectFiles)
            futures.append(executor.submit(TargetFille.SumAllReads, ObjectFiles))

        for future in concurrent.futures.as_completed(futures):
            try:
                newlist, newSuma, newmaximum, newminimum=future.result()
                if newlist!=[]: 
                    ListOfFiles.extend(newlist)
                    suma+=newSuma
                    if maximum<newmaximum or nee:
                        maximum=newmaximum
                    if newminimum<minimum or nee:
                        minimum=newminimum
                    if nee:   
                        nee=False
            except Exception as e:
                print("Thread Error.", e)
   
   
    
    
   
        
    ListOfFiles=TargetFille.nextdtf(TargetFille.deleteEmpty(ListOfFiles))
    
    TargetFille.returnList(ListOfFiles)
    TargetFille.pieChart(len(Files),ListOfFiles)

    TargetFille.barChart(Files,ListOfFiles)




if __name__ == "__main__":
    main()