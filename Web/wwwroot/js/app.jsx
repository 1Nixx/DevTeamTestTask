class Hello extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: props.phone };
        this.onClick = this.onClick.bind(this);
    }
    onClick(e) {
        this.setState({ data: data + 1  })
    }
    render() {
        return <div>
            <p><b>{this.state.data}</b></p>
            <p>Цена {this.state.data}</p>
            <p><button onClick={this.onClick}>Удалить</button></p>
        </div>;
    }
}
ReactDOM.render(
    <Hello key={5} phone = { 444} />,
    document.getElementById("root")
);