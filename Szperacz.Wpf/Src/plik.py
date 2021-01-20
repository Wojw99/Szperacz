import os, fitz
from docx import Document
import matplotlib.pyplot as plt
import concurrent.futures
import regex
from multiprocessing import Process, Manager, Lock, Array, Pool, freeze_support
from multiprocessing.managers import BaseManager

class DUAL:
    def __init__(self,ile:int,ext:str,name=None):
        self.ile = ile
        self.ext = ext
        self.name = name

class TargetFille:
    def __init__(self,FilesTab,Target):
        self.FilesTab=FilesTab
        self.Target=Target

        
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

        with open("config.txt","r", encoding="utf-8") as config:
            txtConfig=TargetFille.NoEnter(config.readlines())
            Target=txtConfig[0]
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
    

    def FindText(self, over=None):
        if over == None:
            FilesTab=self.FilesTab[0]
        else:
            FilesTab=over
        Target=self.Target
        matches_count = 0
        
        if FilesTab!=[]:
            for file in FilesTab:
                try:
                    f = open(file,"r",encoding="utf-8")
                    lines = f.readlines()
                    f.close()

                    text = ""
                    for line in lines:
                        text += line
                    text = text.replace('\n', " ")
                    matches_count = regex.MATCH(text,Target)

                    #print("Found in txt:",matches_count)

                except Exception as e:
                    print("Error in txtReader",e)
        return DUAL(matches_count,"txt",over)
    
    def FindDocx(self, over=None):
        
        if over == None:
            FilesTab=self.FilesTab[1]
        else:
            FilesTab=over
        Target=self.Target
        matches_count = 0
        
        if FilesTab!=[]:
            for files in FilesTab:
                try:
                    doc = Document(files)
                    text = ""
                    for para in doc.paragraphs:
                        text += para.text
                    text.replace("\n"," ")
                    
                    matches_count = regex.MATCH(text,Target)

                    #print("Found in docx:",matches_count)

                except Exception as e:
                        print("Error in docxReader:",e)
        return DUAL(matches_count,"doc",over)
    
    
    def FindPdf(self, over=None):

        if over == None:
            FilesTab=self.FilesTab[2]
        else:
            FilesTab=over
        Target=self.Target
        matches_count = 0
        
        if FilesTab!=[]:
            for files in FilesTab:
                try:
                    pdf = fitz.open(files)
                    pdfSize=pdf.pageCount

                    text = ""

                    for i in range(pdfSize):
                        page = pdf.loadPage(i)
                        text += page.getText("text")
                        
                    text.replace("\n"," ")

                    matches_count = regex.MATCH(text,Target)

                    #print("Found in pdf:",matches_count)
                except Exception as e:
                        print("Error in pdfReader: ",e)
        return DUAL(matches_count,"pdf",over)

        
            
            


def launch(self,ext,over=None):
    if ext == 0:
        #print("Szukam txt")
        return self.FindText([over])
    elif ext == 1:
        #print("Szukam docx")
        return self.FindDocx([over])
    elif ext == 2:
        #print("Szukam pdf")
        return self.FindPdf([over])
    else:
        print("Coś Ci się pojebało stary, chcesz wpierdol?")









def pieChart(tab): #ile wszystkich znalezionych; wszystkie które mają cokolwiek
        allnr = len(tab)
        label=[]
        amount=[]
        sal=0

        for i in range(len(tab)):
            if (tab[i][1] > 0):
                sal += 1
                if tab[i][1] in label:
                    amount[label.index(tab[i][1])]+=1
                else:
                    label.append(tab[i][1])
                    amount.append(1)

        #print(label)
        #print(amount)
        #print(sal)

        allnr-=sal
        if allnr>0:
            label.append(0)
            amount.append(allnr)
            #print(allnr)
                        
                        
                        
        fig1, ax1 = plt.subplots()
        ax1.pie(amount, labels=label, autopct='%1.1f%%',startangle=90)
        ax1.axis('equal')
        
        #plt.show()#zminić na zapis
        plt.savefig("pieChart.png",dpi=200)
        
        
def barChart(tab): #wszystkie pliki, wszystkie gdzie coś znalezione

        amount=[0,0,0]
        allamount=[0,0,0]

        for i in range(len(tab)):
            if tab[i][2] == "txt" and tab[i][1] > 0:
                amount[0] += 1
            elif tab[i][2] == "doc" and tab[i][1] > 0:
                amount[1] += 1
            elif tab[i][2] == "pdf" and tab[i][1] > 0:
                amount[2] += 1
            else:
                print("Zero samuraju, całe zero...")
        
        
        #print(amount)

        for i in range(len(tab)):
            if tab[i][2] == "txt":
                allamount[0] += 1
            elif tab[i][2] == "doc":
                allamount[1] += 1
            elif tab[i][2] == "pdf":
                allamount[2] += 1
            else:
                print("Zero samuraju, całe zero...")

        allamount[0] -= amount[0]
        allamount[1] -= amount[1]
        allamount[2] -= amount[2]
        #print(allamount)

        
    
        labels = ['.txt', '.doc', '.pdf']
    
    
        width = 0.35       # the width of the bars: can also be len(x) sequence
        
        fig, ax = plt.subplots()
        
        ax.bar(labels, amount, width, label='Detected')
        ax.bar(labels, allamount, width, bottom=amount,color="gray",label='All')
        
        ax.set_ylabel('Number of files')
        ax.set_title('Distribution by extensions')
        ax.legend()
        
        #plt.show()
        plt.savefig("barChart.png",dpi=200)
        
	
def axischar(tab):
        maximum = tab[0][1]
        minimum = tab[0][1]
        ilosc = len(tab)
        for i in range(1,len(tab)):
            if tab[i][1] > maximum:
                maximum = tab[i][1]
            if tab[i][1] < minimum:
                minimum = tab[i][1]


        sred=maximum+minimum
        sred/=ilosc
        #print(minimum,maximum,sred)
        
        
        fig, ax = plt.subplots()
        ax.plot([maximum,minimum],[0,0], label="przestrzeń na której są wyniki",linewidth=2.0)
        ax.scatter([sred], [0],270, label="Srednia",marker="|",color="red")
        ax.legend() 
        plt.gca().axes.yaxis.set_ticklabels([])
        
        #plt.show()
        plt.savefig("axisChart.png",dpi=200)





        


MAX_CPU_CORES = 16


if __name__ == "__main__":
    freeze_support()

    Target, Size, Files, IgnoreSize, ThreadNumber=TargetFille.ConfigurationFile()
    main_thread = TargetFille(Files, Target)
    

    AllTargets = []
    for i in range(len(Files[0])):
        AllTargets.append(Files[0][i])
    for i in range(len(Files[1])):
        AllTargets.append(Files[1][i])
    for i in range(len(Files[2])):
        AllTargets.append(Files[2][i])

    cores_used = len(AllTargets)
    print("Files count: ",cores_used)

    cores_used = min(cores_used,MAX_CPU_CORES)

    print("Used cores: ",cores_used)

    pool = Pool(cores_used)

    results = []
    
    for i in range(len(AllTargets)):
        target = AllTargets[i].split(".")[len(AllTargets[i].split("."))-1]
        if (target.find("txt") != -1):
            results.append(pool.apply_async(launch,args=(main_thread,0,AllTargets[i])))
        elif (target.find("doc") != -1):
            results.append(pool.apply_async(launch,args=(main_thread,1,AllTargets[i])))
        elif (target.find("pdf") != -1):
            results.append(pool.apply_async(launch,args=(main_thread,2,AllTargets[i])))
        else:
            print("Hola hola samuraju  mamy",target, "do spalenia")

    pool.close()
    pool.join()
    
    resultt = [result.get() for result in results]

    result = resultt

    print("Output:")
    Files = []
    for i in result:
        Files.append([i.name[0],i.ile,i.ext])
        #print(i.ile,i.ext,i.name[0])


    print("Saving output")

    try:
        f = open("connector.txt","w+",encoding="utf-8")
        for i in range(len(Files)):
            if Files[i][1] > 0:
                f.write(Files[i][0]+"; "+str(Files[i][1])+"\n")
        f.close()
    except:
        print("Coś się zjebało upsik")


    barChart(Files)
    axischar(Files)
    pieChart(Files)
                    