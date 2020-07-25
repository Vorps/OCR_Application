%Fonction descriteur d'intersection XY
function nb = Intersection(image, mode)
    nb = 0;
    if mode == 'Y'
        image = image';
    end
    for i = 1:28
        if image(i, 14) == 0
           nb = nb+1; 
        end
    end
end