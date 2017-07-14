# CNN Image Label Generator
Generates label files for images, which are used for training. This one is specific for YOLO, but could likely be adapted for other image detection convolutional neural network frameworks.

This is based on classifing images within bounding boxes whinin an image. In the user interface the user draws out the bounding box of the object or class for which they wish to classify. Multiple classes and/or bounding boxes can be defined within a single image.

# How to use:

Custom class(es) for each image:

1. Open image(s).
2. Type name of the class you wish to define in textbox and click "Add".
3. Select the newly created class in the listbox.
4. Draw the bounding box within the image.
5. Add additional bounding boxes by adding them to the listbox or click "Next" to move on to the next image.

many bounding boxes can be can be defined in one image for the same or different classes.

Setting the class name using the image name:

1. Check the "Auto-name" checkbox.
2. Open image(s).
3. Select class from listbox and draw bounding box, or if the object consumes the entire image click "Next" to move on to the next image.

Setting the same class name for multiple images:

1. Type the name of class in the textbox for which you wish to define.
2. Check the "Same-name" checkbox.
3. Open image(s).
4. Select class from listbox and draw bounding box, or if the object consumes the entire image click "Next" to move on to the next image.

Saving Label:

1. Once you defined your respective classes and bounding boxes(i.e. above), click "Show Results".
2. Manually create a directory in the same folder as the images called "Labels".
3. Click "Write Labels", and it will write the label files in the the "Labels" directory.
4. To get the "names" file required for training, click the "Get Classes" button, and copy the entries to your blank "names" file.
