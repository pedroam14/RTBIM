import numpy as np
import random as rd
import pandas as pd    
data = pd.read_csv('Assets\MetaData\Metadata1.csv',sep = ';')
print(data)
x = data["Cost"]
print (x)
for i in range(1,17):
    x = data[data.columns[i]]
    for j in range (0,len(x)-1):
        if(x[j] is not None):
            x[j] = rd.randint(0,400)
    print (x)