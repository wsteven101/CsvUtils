
<h1>
C# CSV Reader
</h1>

<body>
    <p></p>
    <h2>Library Description</h2>
    <p>This library provides a CSV (comma separated) stream read which can for example be used to read a file, string or other stream returning a record for each line.</p>
    <p>

    </p>
    <h3>Class: CsvRecord</h3>
    </p>
    <p>This class contains details of each field within a given csv line storing their values as strings accesible by type methods e.g. GetInt(fieldName)</p>
    <p></p>

    <h3>Class: CsvReader</h3>
    <p>Reads a stream (e.g. file) line by line and returns an instance of CsvRecord class for each line.</p>
    <p></p>

    <h3>Class: RecordReaderBase<T></h3>
    <p>Base class for a type safe version of CsvReader. A templated abstract class which takes a CsvReader instance, injected via the constructor, and provides the same interface as the CsvReader class except that it returns records of type T. This class should be inherited from for a specif record type. The derived class's constructor should call this base class's constructor with an instance of ICsvReader. The derived class should also override the ToRecord() function to provide a conversion method that takes a CsvRecord and returns an instance of type T.</p>
    <p></p>

    <h2>Example of a Typed Csv Reader</h2>
    <h3>Class: TradeRecordReader</h3>
    <p>A derivation of the generic base class RecordReaderBase for records (classes) of type TradeRecord. It provides the same interface as the CsvRecord except it returns TradeRecord instead of CsvRecord</p>


    <p></p>
    <h2>Future Improvements</h2>
    <p>Make asynchronous methods/class for file access</p>
</body>
