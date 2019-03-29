Blockly.Blocks['powerpoint'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("PowerPoint");
    this.appendDummyInput()
        .appendField("Fichier :")
        .appendField(new Blockly.FieldTextInput("path"), "POWERPOINT_NAME");
    this.appendStatementInput("POWERPOINT_STATEMENT")
        .setCheck("Array")
        .appendField("Projeter sur :");
    this.setInputsInline(false);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(120);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['audio'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Audio");
    this.appendDummyInput()
        .appendField("Fichier :")
        .appendField(new Blockly.FieldTextInput("path"), "AUDIO_NAME");
    this.appendStatementInput("AUDIO_STATEMENT")
        .setCheck("String")
        .appendField("Ecouter sur :");
    this.setInputsInline(false);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(60);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['video'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Vidéo");
    this.appendDummyInput()
        .appendField("Fichier :")
        .appendField(new Blockly.FieldTextInput("path"), "VIDEO_NAME");
    this.appendStatementInput("VIDEO_STATEMENT")
        .setCheck("Array")
        .appendField("Regarder sur :");
    this.setInputsInline(false);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(15);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['display'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Diffuseur vidéo");
    this.appendDummyInput()
        .appendField(new Blockly.FieldDropdown([["Display1","DISPLAY1"], ["Display2","DISPLAY2"], ["Display3","DISPLAY3"], ["Display4","DISPLAY4"], ["Optoma1","DISPLAY5"], ["Optoma2","DISPLAY6"]]), "DISPLAY_NAME");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(30);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['sound'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Diffuseur audio");
    this.appendDummyInput()
        .appendField(new Blockly.FieldDropdown([["Denon","SOUND1"]]), "SOUND_NAME");
    this.setPreviousStatement(true, null);
    this.setColour(180);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['scene'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Nom de la scène :")
        .appendField(new Blockly.FieldTextInput("default"), "SCENE_NAME");
    this.appendStatementInput("SCENE_STATEMENT")
        .setCheck(null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['sceneactions'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Nom du groupe d'actions :")
        .appendField(new Blockly.FieldTextInput("default"), "SCENEACTIONS_NAME");
    this.appendStatementInput("SCENEACTIONS_STATEMENT")
        .setCheck(null);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};