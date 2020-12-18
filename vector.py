from PIL import Image
from numpy import asarray
import numpy as np

def newimage(txt,n):
    img = Image.new('L', (200, 200))
    k=[]
    for x in range(200*200):
        k.append(n)
    img.putdata(k)
    img.save('b_'+txt)






#  Min/Max/Sum/Median/    /Standard Deviation
image = Image.open('orginal_b.jpg').convert('L')

data = asarray(image)


print(np.amin(data))
newimage('min_image.jpg', np.amin(data))
print(np.amax(data))
newimage('max_image.jpg', np.amax(data))
print(np.median(data))
newimage('median_image.jpg', np.median(data))
print(np.mean(data))
newimage('mean_image.jpg', np.mean(data))
print(np.std(data))
newimage('SD_image.jpg', np.std(data))




