file="normaldist.txt"

binwidth=5
bin(x,width)=width*floor(x/width)

plot file using (bin($1,binwidth)):(1.0) smooth freq with boxes